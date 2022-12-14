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

namespace Game.Characters.States
{
    [Register]
    internal class MonsterAppearState : BaseState
    {
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

        private protected override void BeforeTerminate()
        {
            _movingAgent.ChangeCurrentRuntime(RuntimeType.PersueWalk);
            _chestEntityBody.SetRigidbodiesEnabled(false);
            _chest.Destroy();
        }

#region KernelEntity

        [ConstructField]
        private IChest _chest;

        private Transform _thisTransform;

        private IBody _chestEntityBody;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _chestEntityBody = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity);
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _thisTransform = _chestEntityBody.Transform;
            _chestEntityBody.SetCollisionDetectorsEnabled(true);
            _chestEntityBody.SetRigidbodiesEnabled(true);
        }

#endregion
    }
}
