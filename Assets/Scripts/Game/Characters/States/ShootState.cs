using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Effects;
using System;
using Game.Characters.Enums;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters.Player;
using Game.Characters.Interfaces;

namespace Game.Characters.States
{
    internal class ShootState : BaseState
    {
        public event Action<float> onMonsterAttackedPlayer;
        
        [SerializeField] private GameObject patron;
        [SerializeField] float timeBeetweenAttacks = 0.5f;

        private float _currentTime = 0f;

        public override StateEntityType StateEntityType { get => StateEntityType.Attack; }

        public override void Run()
        {
            if (_currentTime <= timeBeetweenAttacks)
            {
                _currentTime += Time.deltaTime;
                return;
            }

            Instantiate(patron);
            _currentTime = 0f;
        }

#region MonoBehaviour

        private void Start()
        {
            _currentTime = timeBeetweenAttacks;
        }

#endregion
    }
}
