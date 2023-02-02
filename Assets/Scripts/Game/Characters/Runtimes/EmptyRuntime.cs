using Game.Characters.Enums;

namespace Game.Characters.Runtimes
{
    internal class EmptyRuntime : BaseRuntime
    {
        public override RuntimeType RuntimeType { get => RuntimeType.Empty; }
        public override void Run()
        {

        }
    }
}
