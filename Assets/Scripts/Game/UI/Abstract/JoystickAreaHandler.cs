using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utilities.Behaviours;
using Utilities.Utils;

namespace Game.UI.Abstract
{
    internal abstract class JoystickAreaHandler : MultiThreadedUpdateMonoBehaviour
    {
        [SerializeField] private Transform joystickTransform;
        [SerializeField] private Transform markerTransform;

        [Space]
        [Header("Joysticks zones")]
        [SerializeField] private UIRectangleZone2D activeJoystickZone;
        [SerializeField] private UICircleZone2D joystickZone;

        private float _joystickTransformPositionZ;
        private float _joystickRadius;
        private Vector2 _joystickStartPosition;

        private Vector2 _joystickUpdatedPosition;

        private bool _updateDirectionInProcess;
        private bool _touchPositionOnCanvas;

        private Vector2 _lastMarkerPosition;

        private void Start()
        {
            _joystickStartPosition = markerTransform.position;
            _joystickUpdatedPosition = _joystickStartPosition;
            _joystickRadius = joystickZone.Radius;

            _joystickTransformPositionZ = transform.position.z;

            StartInternal();
        }

        private protected virtual void UpdateDirection(in Vector3 direction) { }

        private Vector2 GetMarkerPositionByDirection(Vector2 direction)
        {
            return _joystickUpdatedPosition + direction.normalized * _joystickRadius;
        }

        private void UpdateJoystickPositionOnClick()
        {
            if (!_updateDirectionInProcess && Input.GetMouseButtonDown(0))
            {
                Vector2 touchPosition = Input.mousePosition;

                if (joystickZone.IsPositionInsideZone(touchPosition))
                {
                    _updateDirectionInProcess = true;
                }

                else if (activeJoystickZone.IsPositionInsideZone(touchPosition))
                {
                    joystickTransform.position = touchPosition;
                    markerTransform.position = touchPosition;
                    _joystickUpdatedPosition = touchPosition;
                    _updateDirectionInProcess = true;
                }

            }

            else if (_updateDirectionInProcess && Input.GetMouseButtonUp(0))
            {
                _joystickUpdatedPosition = _joystickStartPosition;
                joystickTransform.position = _joystickStartPosition;
                markerTransform.position = _joystickStartPosition;
                _updateDirectionInProcess = false;
            }
        }

        private void UpdateDirection()
        {
            if (!_updateDirectionInProcess)
            {
                return;
            }

            Vector2 mousePosition = Input.mousePosition;

            Vector2 direction = mousePosition - _joystickUpdatedPosition;
            UpdateDirection(direction);

            markerTransform.position = joystickZone.IsPositionInsideZone(mousePosition)
                ? mousePosition
                : GetMarkerPositionByDirection(direction);
        }

#region Update

        private protected override void SetUpUpdateSettings()
        {
            _updateThreads = new List<InvokeRepeatingSettings> {
            new InvokeRepeatingSettings(nameof(UpdateJoystickPositionOnClick)),
            new InvokeRepeatingSettings(nameof(UpdateDirection))
        };
        }

#endregion
    }
}
