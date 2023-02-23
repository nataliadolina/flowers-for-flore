using UnityEngine;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Run;
using Game.Characters.Utilities.Utils.Delegates;
using Game.Characters.Interfaces;
using System;
using Game.Characters.Player;

namespace Game.Characters.Handlers
{
    internal class PatronCollisionDetector : MonoBehaviour, ICausePlayerDamage
    {
        public event Action<float> onCausedPlayerDamage;

        [SerializeField]
        private float harm;
        
        private void OnCollisionEnter(Collision collision)
        {
            Collider collider = collision.collider;
            if (collider.GetComponent<PlayerMovement>())
            {
                onCausedPlayerDamage?.Invoke(harm);
            }
        }
    }
}
