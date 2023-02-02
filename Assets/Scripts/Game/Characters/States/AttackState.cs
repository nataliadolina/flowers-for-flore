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
        private bool _canAttack = false;

        private Particles[] _collisionParticles;

        public override StateEntityType StateEntityType { get => StateEntityType.Attack; }

        public override void Run()
        {
            if (!_canAttack || _currentTime < timeBeetweenAttacks)
            {
                _currentTime += Time.deltaTime;
                return;
            }

            onMonsterAttackedPlayer(harm);
            _currentTime = 0f;

            //int index = UnityEngine.Random.Range(0, _collisionParticles.Length);
            //_collisionParticles[index].IsActivated = true;
        }

        private void OnPlayerEnteredAttackZone(float distance)
        {
            _canAttack = true;
        }

        private void OnPlayerExitAttackZone()
        {
            _canAttack = false;
        }


#region MonoBehaviour

        private void Start()
        {
            _collisionParticles = GetComponentsInChildren<Particles>();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Kernel Entity

        private IDistanceToSubjectZoneProcessor _distanceToPlayerProcessor;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _distanceToPlayerProcessor = kernel.GetInjection<IDistanceToSubjectZoneProcessor>(x => x.OwnerType == OwnerType.ChestEntity && x.AimType == OwnerType.Player);
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _distanceToPlayerProcessor.onAimEnterZone += OnPlayerEnteredAttackZone;
            _distanceToPlayerProcessor.onAimExitZone += OnPlayerExitAttackZone;
        }

        private void ClearSubscriptions()
        {
            _distanceToPlayerProcessor.onAimEnterZone -= OnPlayerEnteredAttackZone;
            _distanceToPlayerProcessor.onAimExitZone -= OnPlayerExitAttackZone;
        }

#endregion
        
    }
}
