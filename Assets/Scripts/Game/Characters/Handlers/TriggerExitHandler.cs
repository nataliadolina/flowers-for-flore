using UnityEngine;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;
using Game.Characters.Player;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;

namespace Game.Characters.Handlers
{
    [Register]
    [Register(typeof(ICollisionDetector))]
    internal class TriggerExitHandler : MonoBehaviour, ICollisionDetector, IKernelEntity
    {
        [SerializeField]
        private StateEntityType toState;

        private Collider _collider;

#region MonoBehaviour

        private void Start()
        {
            _collider = GetComponent<Collider>();
        }

#endregion

#region ICollisionDetector

        public void ColliderSetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }

#endregion

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                _movingAgent.ChangeCurrentState(toState);
                Debug.Log($"Changed to state {_movingAgent.CurrentState}");
            }
        }

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

#endregion
    }
}
