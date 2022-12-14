using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Handlers.Abstract;
using DI.Attributes.Run;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.Utilities.Utils;
using System;

namespace Game.Characters.Handlerss
{
    [Register]
    [Register(typeof(IDistanceToPlayerHandler))]
    [Register(typeof(ICollisionDetector))]
    internal class DistanceToPlayerHandler : HandlerBase, IDistanceToPlayerHandler, IKernelEntity
    {
        public event Action<DistanceToPlayerArgs> onDistanceToPlayerChange;
        [SerializeField] private float maxVisibleDistance = 10;

        private float _currentDistance;

        private bool _isDetectionAreaActive = true;

        private bool _isPlayerVisible = false;

        private bool IsPlayerVisible
        {
            get => _isPlayerVisible;
            set
            {
                if (_isPlayerVisible != value)
                {
                    _isPlayerVisible = value;
                    onDistanceToPlayerChange(new DistanceToPlayerArgs(_currentDistance, _chest, value));
                }
            }
        }

        private void Update()
        {
            if (!_isDetectionAreaActive)
            {
                IsPlayerVisible = false;
                return;
            }

            _currentDistance = Vector3.Distance(_playerTransform.position, _chestEntityTransform.position);
            if (_currentDistance <= maxVisibleDistance){
                IsPlayerVisible = true;
            } else
            {
                IsPlayerVisible = false;
            }
        }

#region ITriggerHandler

        public void ColliderSetEnabled(bool isEnabled)
        {
            _isDetectionAreaActive = isEnabled;
        }

#endregion

#region KernelEntity

        [ConstructField]
        private IChest _chest;

        private Transform _chestEntityTransform;

        private Transform _playerTransform;

        [ConstructField(KernelTypeOwner.Player)]
        private Player player;

        [RunMethod]
        private void OnConstruct(IKernel kernel)
        {
            _playerTransform = player.transform;
            _chestEntityTransform = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity).Transform;
        }

#endregion

#if UNITY_EDITOR

        void OnDrawGizmosSelected()
        {
            if (!_isDetectionAreaActive)
            {
                return;
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, maxVisibleDistance);
        }

#endif

    }
}
