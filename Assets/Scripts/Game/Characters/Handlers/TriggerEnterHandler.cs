using UnityEngine;
using Game.Characters.Handlers.Abstract;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Run;
using Game.Characters.Utilities.Utils.Delegates;
using Game.Characters.Interfaces;
using Game.Characters.Player;
using System;


namespace Game.Characters.Handlers
{   
    [Register]
    [Register(typeof(IPlayerContact))]
    [Register(typeof(ICollisionDetector))]
    internal class TriggerEnterHandler : MonoBehaviour, IKernelEntity, IPlayerContact
    {
        public event OnChestEntityContactedPlayer onChestEntityContancedPlayer;
        public event Action<Transform> onFlowerContancedPlayer;
        private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerHandler>())
            {
                onChestEntityContancedPlayer(_chestEntityTransform, _ownerType, _movingAgent.CurrentState.StateEntityType);
                _movingAgent.CurrentState.Terminate();
            }

            if (_ownerType == OwnerType.Flower && other.GetComponent<FlowersContainer>())
            {
                
            }
        }

#region ITriggerHandler

        public void ColliderSetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }

#endregion

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

        private IBody _chestEntityTransform;

        private OwnerType _ownerType;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _collider = GetComponent<Collider>();
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _chestEntityTransform = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity);
            _ownerType = kernel.GetInjection<IChestEntity>().OwnerType;
        }

#endregion

    }
}
