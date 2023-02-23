using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.States.Managers;
using Game.Characters.Interfaces;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using Game.Characters.Abstract;
using Game.Characters.Enums;
using System;

namespace Game.Characters.States
{
    [Register]
    [Register(typeof(IAppearState))]
    internal class MonsterAppearState : BaseState, IAppearState
    {
        public event Action onAppearStateTerminated;

        [SerializeField]
        private float speed;

        private float _lastPositionY;

        public override StateEntityType StateEntityType { get => StateEntityType.Appear; }

        public override void Run()
        {
            float currentY = _thisTransform.position.y;
            if (currentY < _lastPositionY)
            {
                Terminate();
            }

            else
            {
                _lastPositionY = currentY;
            }
        }

        public override void OnStartState()
        {
            _creatureBody.SetRigidbodiesEnabled(true);
            _chestEntityRigidbody.AddForce(Vector3.up * speed * 0.02f, ForceMode.Impulse);
        }

        public override void BeforeTerminate()
        {
            onAppearStateTerminated?.Invoke();
            _creatureBody.SetRigidbodiesEnabled(false);
        }

#region KernelEntity

        private Transform _thisTransform;

        private IBody _creatureBody;

        private Rigidbody _chestEntityRigidbody;

        [ConstructField]
        private IStateManager _stateManager;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _creatureBody = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.Creature);
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _thisTransform = _creatureBody.Transform;
            _chestEntityRigidbody = _creatureBody.Rigidbody;
        }

#endregion
    }
}
