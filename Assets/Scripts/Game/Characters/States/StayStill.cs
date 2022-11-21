using Game.Characters.States.Abstract;
using Game.Characters.Enums;
using UnityEngine;
using DI.Attributes.Register;

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
    }
}
