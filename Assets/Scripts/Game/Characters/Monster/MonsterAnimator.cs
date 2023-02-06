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

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

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
            Debug.Log("Animator set index attack");
        }

        private void OnCurrentStateChanged(StateEntityType stateType)
        {
            switch (stateType)
            {
                case StateEntityType.Walk: { Walk(); break; }
                case StateEntityType.Persue: { Run(); break; }
            }
        }

#region Kernel Entity

        [ConstructField]
        private IMonsterAttack _monsterAttack;

        [ConstructField]
        private MovingAgent _movingAgent;

        private Animator _animator;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _animator = GetComponent<Animator>();
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _monsterAttack.onMonsterAttackedPlayer += Attack;
            _movingAgent.onCurrentStateChanged += OnCurrentStateChanged;
        }

        private void ClearSubscriptions()
        {
            _monsterAttack.onMonsterAttackedPlayer -= Attack;
            _movingAgent.onCurrentStateChanged -= OnCurrentStateChanged;
        }

#endregion
    }
}
