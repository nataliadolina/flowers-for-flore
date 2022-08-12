using Game.Characters.States.Managers;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using Game.Characters.Runtimes.Interfaces;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;

namespace Game.Characters.Runtimes
{
    [Register(typeof(IRuntime))]
    internal class CurrentStateRuntime : IRuntime, IKernelEntity
    {
        public RuntimeType RuntimeType { get => RuntimeType.CurrentState; }

        public void Run()
        {
            _movingAgent.CurrentState.Run();
        }

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

#endregion

    }
}
