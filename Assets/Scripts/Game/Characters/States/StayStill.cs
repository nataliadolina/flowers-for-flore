using Game.Characters.States.Abstract;
using Game.Characters.Enums;
using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;

namespace Game.Characters.States
{
    [Register]
    internal class StayStill : BaseState
    {
        [SerializeField] private StateEntityType stateEntityType = StateEntityType.Still;
        public override StateEntityType StateEntityType { get => stateEntityType; }

        public override void Run()
        {

        }

#region Kernel Entity

        private IBody _body;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _body = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity);
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _body.SetRigidbodiesEnabled(false);
        }

#endregion

    }
}
