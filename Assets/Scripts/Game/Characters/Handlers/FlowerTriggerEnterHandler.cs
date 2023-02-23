using UnityEngine;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Run;
using Game.Characters.Utilities.Utils.Delegates;
using Game.Characters.Interfaces;
using System;


namespace Game.Characters.Handlers
{
    [Register]
    [Register(typeof(IFlowerTriggerEnterHandler))]
    [Register(typeof(ICollisionDetector))]
    internal class FlowerTriggerEnterHandler : MonoBehaviour, IKernelEntity, IFlowerTriggerEnterHandler, ICollisionDetector
    {
        public event Action<IBody> onFlowersContainerContact;
        private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<FlowersContainer>())
            {
                _movingAgent.CurrentState.Terminate();
                onFlowersContainerContact?.Invoke(_chestEntityBody);
            }
        }

#region ICollisionDetector

        public void ColliderSetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }

#endregion

#region Kernel Entity
        [ConstructField]
        private MovingAgent _movingAgent;

        private IBody _chestEntityBody;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _collider = GetComponent<Collider>();
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _chestEntityBody = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.Creature);
        }

#endregion
    }
}