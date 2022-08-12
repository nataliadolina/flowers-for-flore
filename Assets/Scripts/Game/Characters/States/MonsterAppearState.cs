using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.States.Managers;
using Game.Characters.Interfaces;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using Game.Characters.Abstract;
using Game.Characters.Enums;

namespace Game.Characters.States
{
    internal class MonsterAppearState : BaseState
    {
        private float _lastPositionY;

        public override StateEntityType StateEntityType { get => StateEntityType.MonsterAppear; }

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

        private protected override void BeforeTerminate()
        {
            _movingAgent.ChangeCurrentRuntime(RuntimeType.PersueWalk);
            _chestEntityPhysics.SetRigidbodiesEnabled(false);
            _chest.Destroy();
        }

#region KernelEntity

        [ConstructField]
        private IChest _chest;

        [ConstructField]
        private MovingAgent _movingAgent;

        private Transform _thisTransform;

        [ConstructField]
        private ChestEntityPhysics _chestEntityPhysics;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _thisTransform = _chestEntityPhysics.transform;
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _chestEntityPhysics.EnableRigidBodiesAndColliders();
        }

#endregion
    }
}
