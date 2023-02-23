using Game.Characters.Enums;

namespace Game.Characters.Interfaces
{
    internal interface IStateEntity
    {
        StateEntityType StateEntityType { get; }

        public void Run();
        public void BeforeTerminate();
        public void Terminate();
        public void OnStartState();
    }
}
