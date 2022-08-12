using Game.Characters.States.Abstract;
using Game.Characters.Enums;

namespace Game.Characters.States
{
    internal class StayStill : BaseState
    {
        public override StateEntityType StateEntityType { get => StateEntityType.Still; }
        public override void Run()
        {

        }
    }
}
