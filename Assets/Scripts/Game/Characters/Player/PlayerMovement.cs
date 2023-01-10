using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI.Interfaces;
using Game.Characters.Interfaces;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;

namespace Game.Characters.Player
{
    [Register]
    internal class PlayerMovement : MonoBehaviour, IKernelEntity
    {
        [SerializeField]
        private float speed;

        private float _speedRatio = 0f;

        private void ApplyDirection(Vector2 direction, bool updateDirectionInProgress)
        {
            _speedRatio = direction.magnitude;
            _animator.SetSpeedRatio(_speedRatio);

            if (updateDirectionInProgress)
            {
                if (direction != Vector2.zero)
                {
                    _playerTransform.forward = new Vector3(direction.x, 0, direction.y);
                }
                _playerTransform.Translate(Vector3.forward * speed * _speedRatio * Time.deltaTime);
            }
        }

#region Kernel Entity

        private Transform _playerTransform;

        [ConstructField]
        private PlayerAnimator _animator;

        [ConstructMethod(KernelTypeOwner.UI)]
        private void OnConstruct(IKernel kernel)
        {
            kernel.GetInjection<IPlayerDirectionInput>().onCharacterDirectionChanged += ApplyDirection;
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _playerTransform = kernel.GetInjection<IBody>().Transform;
        }

#endregion
    }
}