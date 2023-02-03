using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using Utilities.Extensions;
using Game.Characters.States.Managers;

namespace Game.Characters.Handlers
{
    [Register(typeof(IDistanceToSubjectZoneProcessor))]
    internal class DistanceToSubjectZoneProcessor : MonoBehaviour, IDistanceToSubjectZoneProcessor, IKernelEntity
    {
        public event Action<float> onAimEnterZone;
        public event Action onAimExitZone;

        [SerializeField] private float maxVisibleDistance;
        [SerializeField] private OwnerType ownerType;
        [SerializeField] private OwnerType aimType;

        private bool _isSubjectInsideZone = false;
        private float _currentDistance;
        private bool _needToSwitchStateIfValueNotChanged = false;

        public bool IsSubjectInsideZone
        {
            get => _isSubjectInsideZone;
            set
            {
                if (_isSubjectInsideZone == value & !_needToSwitchStateIfValueNotChanged)
                {
                    return;
                }

                if (!value)
                {
                    onAimExitZone?.Invoke();
                }
                else if (value)
                {
                    onAimEnterZone?.Invoke(_currentDistance);
                }
                _isSubjectInsideZone = value;
                _needToSwitchStateIfValueNotChanged = false;
            }
        }

#region Owner Type

        public OwnerType OwnerType { get=> ownerType; }

#endregion

#region Aim Type

        public OwnerType AimType { get => aimType; }

#endregion

        private void SetIsSubjectInsideZone(float distanceToAim)
        {
            _currentDistance = distanceToAim;
            IsSubjectInsideZone = distanceToAim <= maxVisibleDistance ? true : false;
        }

        private void SetNeedToSwitchStateIfValueNotChanged(StateEntityType stateType)
        {
            if (stateType == StateEntityType.Walk)
            {
                _needToSwitchStateIfValueNotChanged = true;
            }
        }

#region KernelEntity

        [ConstructField]
        private IDistanceToSubjectDetector _distanceToSubjectDetector;

        [ConstructField]
        private MovingAgent _movingAgent;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _distanceToSubjectDetector = kernel.GetInjection<IDistanceToSubjectDetector>(
                x => x.AimType == aimType && x.OwnerTypes.HasValue(ownerType.ToString())
                );
            SetSubscriptions();
        }

        private void SetSubscriptions()
        {
            _distanceToSubjectDetector.onDistanceToSubjectChange += SetIsSubjectInsideZone;
            _movingAgent.onCurrentStateChanged += SetNeedToSwitchStateIfValueNotChanged;
        }

#endregion

#if UNITY_EDITOR

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, maxVisibleDistance);
        }

#endif
    }
}
