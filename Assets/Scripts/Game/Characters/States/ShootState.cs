using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Effects;
using System;
using Game.Characters.Enums;
using Utilities;
using DI.Attributes.Construct;
using Game.Characters.ShootSystem;

namespace Game.Characters.States
{
    internal class ShootState : BaseState
    {
        public event Action<float> onMonsterAttackedPlayer;
        
        [SerializeField] float timeBeetweenAttacks = 0.5f;

        private float _currentTime = 0f;

        public override StateEntityType StateEntityType { get => StateEntityType.Attack; }

        public override void Run()
        {
            if (_currentTime <= timeBeetweenAttacks)
            {
                _currentTime += GameParams.UPDATE_RATE;
                return;
            }

            _currentTime = 0f;
            _gunHandler.Shoot();
        }

#region MonoBehaviour

        private void Start()
        {
            _currentTime = timeBeetweenAttacks;
        }

#endregion

#region Kernel Entity

        [ConstructField]
        private GunHandler _gunHandler;

#endregion
    }
}
