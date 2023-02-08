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
    internal class MonsterAppearState : BaseState
    {
        private float _lastPositionY;
        private bool _startUpdate = false;

        public override StateEntityType StateEntityType { get => StateEntityType.Appear; }

        public override void Run()
        {
            if (!_startUpdate)
            {
                return;
            }

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
            _chestEntityBody.SetRigidbodiesEnabled(true);
            _chestEntityRigidbody.AddForce(Vector3.up * speed * 0.02f, ForceMode.Impulse);
            _startUpdate = true;
        }

        private protected override void BeforeTerminate()
        {
            Debug.Log($"Change current state runtime to {RuntimeType.PersueWalk}");
            _movingAgent.ChangeCurrentRuntime(RuntimeType.PersueWalk);
            _chestEntityBody.SetRigidbodiesEnabled(false);
        }

#region KernelEntity

        private Transform _thisTransform;

        private IBody _chestEntityBody;

        private Rigidbody _chestEntityRigidbody;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _chestEntityBody = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity);
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _thisTransform = _chestEntityBody.Transform;
            _chestEntityRigidbody = _chestEntityBody.Rigidbody;
        }

#endregion
    }
}
