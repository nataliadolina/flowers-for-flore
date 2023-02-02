using UnityEngine;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Run;
using Game.Characters.Utilities.Utils.Delegates;
using Game.Characters.Interfaces;
using Game.Characters.Player;
using System;


namespace Game.Characters.Handlers
{   
    [Register]
    [Register(typeof(ICollisionDetector))]
    internal class MonsterTriggerEnterHandler : MonoBehaviour, IKernelEntity, ICollisionDetector
    {
        private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                _movingAgent.CurrentState.Terminate();
                Debug.Log($"Changed to state {_movingAgent.CurrentState}");
            }
        }

#region ITriggerHandler

        public void ColliderSetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }

#endregion

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _collider = GetComponent<Collider>();
        }

#endregion

    }
}
