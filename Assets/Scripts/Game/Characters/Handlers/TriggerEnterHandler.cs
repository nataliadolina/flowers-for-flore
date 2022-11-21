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
            Debug.Log(other);
            if (other.GetComponent<Player>())
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
