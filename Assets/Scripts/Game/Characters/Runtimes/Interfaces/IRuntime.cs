using Game.Characters.Enums;

namespace Game.Characters.Runtimes.Interfaces
{
    internal interface IRuntime
    {
        RuntimeType RuntimeType { get; }
        void Run();
    }
}
