using UnityEngine;
using Game.Characters.Handlers.Abstract;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using Game.Characters.Utilities.Utils.Delegates;
using Game.Characters.Interfaces;


namespace Game.Characters.Handlers
{   
    [Register]
    [Register(typeof(IPlayerContact))]
    internal class TriggerEnterHandler : MonoBehaviour, IKernelEntity, IPlayerContact
    {
        public event OnChestEntityContactedPlayer onChestEntityContancedPlayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                _movingAgent.CurrentState.Terminate();
                onChestEntityContancedPlayer(_chestEntityTransform, _ownerType);
            }
        }

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

        private Transform _chestEntityTransform;

        private OwnerType _ownerType;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _chestEntityTransform = kernel.GetInjection<IBody>().Transform;
            _ownerType = kernel.GetInjection<IOwnerType>().OwnerType;
        }

#endregion

    }
}
