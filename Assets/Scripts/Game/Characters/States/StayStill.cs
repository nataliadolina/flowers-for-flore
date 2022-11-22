using Game.Characters.States.Abstract;
using Game.Characters.Enums;
using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
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

        public override void OnStartState()
        {
            _body.SetCollidersEnabled(false);
            _body.SetRigidbodiesEnabled(false);
        }

#region Kernel Entity

        [ConstructField]
        private IBody _body;

#endregion

    }
}
