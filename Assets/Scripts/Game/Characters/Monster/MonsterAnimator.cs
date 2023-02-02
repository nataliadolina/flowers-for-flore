using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using Game.Characters.Interfaces;
using Game.Characters.States.Managers;
using Game.Characters.Enums;

namespace Game.Characters.Monster
{
    [Register]
    internal class MonsterAnimator : MonoBehaviour, IKernelEntity
    {
        private readonly int WalkIndex = Animator.StringToHash("Walk");
        private readonly int RunIndex = Animator.StringToHash("Run");
        private readonly int AttackIndex = Animator.StringToHash("Attack");

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Walk()
        {
            _animator.SetTrigger(WalkIndex);
        }

        private void Run()
        {
            _animator.SetTrigger(RunIndex);
        }

        private void Attack(float harm)
        {
            _animator.SetTrigger(AttackIndex);
        }

        private void OnCurrentStateChanged(StateEntityType stateType)
        {
            switch (stateType)
            {
                case StateEntityType.Walk: { Walk(); break; }
                case StateEntityType.Persue: { Run(); break; }
            }
        }

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            kernel.GetInjection<IMonsterAttack>().onMonsterAttackedPlayer += Attack;
            kernel.GetInjection<MovingAgent>().onCurrentStateChanged += OnCurrentStateChanged;
        }
    }
}