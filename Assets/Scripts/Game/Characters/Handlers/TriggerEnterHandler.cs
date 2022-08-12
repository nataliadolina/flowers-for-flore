using UnityEngine;
using Game.Characters.Handlers.Abstract;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;


namespace Game.Characters.Handlers
{
    internal class TriggerEnterHandler : HandlerBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (check(other.transform))
            {
                _movingAgent.CurrentState.Terminate();
            }
        }

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

#endregion

    }
}
