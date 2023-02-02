using Game.Characters.States.Managers;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using Game.Characters.Runtimes.Interfaces;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using UnityEngine;

namespace Game.Characters.Runtimes
{
    internal class CurrentStateRuntime : BaseRuntime
    {
        public override RuntimeType RuntimeType { get => RuntimeType.CurrentState; }

        public override void Run()
        {
            _movingAgent.CurrentState.Run();
        }

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

#endregion

    }
}
