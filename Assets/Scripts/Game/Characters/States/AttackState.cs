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
    [Register(typeof(IMonsterAttack))]
    internal class AttackState : BaseState, IMonsterAttack
    {
        public event Action<float> onMonsterAttackedPlayer;

        [SerializeField] private float harm;
        [SerializeField] float timeBeetweenAttacks = 1f;

        private float _currentTime = 0f;

        public override StateEntityType StateEntityType { get => StateEntityType.Attack; }

        public override void Run()
        {
            if (_currentTime < timeBeetweenAttacks)
            {
                _currentTime += Time.deltaTime;
                return;
            }

            onMonsterAttackedPlayer(harm);
            _currentTime = 0f;
        }
        
    }
}
