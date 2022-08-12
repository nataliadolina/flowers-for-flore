using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Effects;
using System;
using Game.Characters.Enums;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;

namespace Game.Characters.States
{
    internal class AttackState : BaseState
    {
        internal event Action<float> onAttacked;

        [SerializeField] private float harm;
        [SerializeField] float timeBeetweenAttacks = 1f;
        [SerializeField] float attackDist = 2f;

        private float _currentTime = 0f;

        private Particles[] _collisionParticles;
        private Animator _animator;

        public override StateEntityType StateEntityType { get => StateEntityType.Attack; }

#region MonoBehaviour

        private void Start()
        {
            _collisionParticles = GetComponentsInChildren<Particles>();
            _animator = GetComponentInParent<Animator>();
        }

#endregion

        public override void Run()
        {
            if (_currentTime >= timeBeetweenAttacks)
            {
                _currentTime = 0f;
                _animator.SetTrigger("Attack");

                var heading = transform.position - _playerTransform.position;
                var distToPlayer = heading.magnitude;

                if (distToPlayer <= attackDist)
                {
                    onAttacked(harm);

                    int index = UnityEngine.Random.Range(0, _collisionParticles.Length);
                    _collisionParticles[index].IsActivated = true;
                }

            }
            _currentTime += Time.deltaTime;
        }

#region Kernel Entity

        [ConstructField]
        private Player _player;

        private Transform _playerTransform;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _playerTransform = _player.transform;
        }

#endregion
    }
}
