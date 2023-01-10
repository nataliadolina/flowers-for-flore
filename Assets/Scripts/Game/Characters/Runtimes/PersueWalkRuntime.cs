﻿using UnityEngine;
using Game.Characters.States.Managers;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using Game.Characters.Runtimes.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Enums;

namespace Game.Characters.Runtimes
{
    [Register(typeof(IRuntime))]
    internal class PersueWalkRuntime : MonoBehaviour, IRuntime, IKernelEntity
    {
        [SerializeField] private float persueDist;

        public RuntimeType RuntimeType { get => RuntimeType.PersueWalk; }

        public void Run()
        {
            var heading = _thisTransform.position - _playerTransform.position;
            var distance = heading.magnitude;

            if (distance < persueDist & _movingAgent.CurrentState.StateEntityType != StateEntityType.Persue)
            {
                _movingAgent.CurrentState = _persueState;
            }

            else
            {
                _movingAgent.CurrentState = _walkState;
            }
            _movingAgent.CurrentState.Run();
        }

#region Kernel entity

        [ConstructField]
        private MovingAgent _movingAgent;
        
        private Transform _playerTransform;

        private Transform _thisTransform;

        private IStateEntity _persueState;

        private IStateEntity _walkState;

        [ConstructField(KernelTypeOwner.Player)]
        private IBody _playerBody;

        [ConstructMethod]
        private void OnRun(IKernel kernel)
        {
            _playerTransform = _playerBody.Transform;
            _thisTransform = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity).Transform;
            _persueState = kernel.GetInjection<IStateEntity>(x => x.StateEntityType == StateEntityType.Persue);
            _walkState = kernel.GetInjection<IStateEntity>(x => x.StateEntityType == StateEntityType.Walk);
        }

#endregion
    }
}
