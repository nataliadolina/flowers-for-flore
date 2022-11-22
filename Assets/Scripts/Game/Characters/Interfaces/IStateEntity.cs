using Game.Characters.Enums;

namespace Game.Characters.Interfaces
{
    internal interface IStateEntity
    {
        StateEntityType StateEntityType { get; }

        void Run();
        void Terminate();
        void OnStartState();
    }
}
