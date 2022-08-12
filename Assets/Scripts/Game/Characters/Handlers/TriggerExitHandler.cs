using UnityEngine;
using Game.Characters.Handlers.Abstract;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;

namespace Game.Characters.Handlers
{
    internal class TriggerExitHandler : HandlerBase
    {
        [SerializeField]
        private StateEntityType toState;

        private void OnTriggerExit(Collider other)
        {
            if (check(other.transform))
            {
                _movingAgent.ChangeCurrentState(toState);
            }
        }

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

#endregion
    }
}
