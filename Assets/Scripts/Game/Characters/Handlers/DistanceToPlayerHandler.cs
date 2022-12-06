using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Handlers.Abstract;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters;
using System;

namespace Game.Characters.Handlers
{
    [Register]
    [Register(typeof(HandlerBase))]
    internal class DistanceToPlayerHandler : HandlerBase
    {
        internal Action<float> onDistanceToPlayerChange;
        [SerializeField] private float maxVisibleDistance = 10;

        private float _currentDistance;

        private bool _isPlayerVisible = false;

        private bool IsPlayerVisible
        {
            get => _isPlayerVisible;
            set
            {
                if (_isPlayerVisible != value)
                {
                    _isPlayerVisible = value;
                    onDistanceToPlayerChange(_currentDistance);
                }
            }
        }
        private void Update()
        {
            _currentDistance = Vector3.Distance(playerTransform.position, thisTransform.position);
            if (_currentDistance <= maxVisibleDistance){
                IsPlayerVisible = true;
            } else
            {
                IsPlayerVisible = false;
            }
        }

#region KernelEntity

        private Transform thisTransform;

        private Transform playerTransform;

        [ConstructField(KernelTypeOwner.Player)]
        private Player player;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            thisTransform = transform;
            playerTransform = player.transform;

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
