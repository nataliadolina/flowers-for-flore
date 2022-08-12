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
    internal class FloatUpState : BaseState
    {
        [SerializeField] private float height = 1f;

        public override StateEntityType StateEntityType { get => StateEntityType.FlowerAppear; }
        public override void Run()
        {
            float delta = _chestEntityTransform.position.y - _startPoint;
            if (delta < height)
            {
                float translateSpeed = speed;
                if (delta > 0.5f * height)
                    translateSpeed = 2 * speed;
                _chestEntityTransform.Translate(Vector3.up * translateSpeed * Time.deltaTime);
            }
            else
            {
                Terminate();
            }
        }

        private protected override void BeforeTerminate()
        {
            _chestEntityPhysics.SetCollidersEnabled(true);
            _chest.Destroy();
        }

#region Kernel Entity

        [ConstructField]
        private ChestEntityPhysics _chestEntityPhysics;

        private Transform _chestEntityTransform;

        [ConstructField]
        private IChest _chest;

        private float _startPoint;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _chestEntityTransform = _chestEntityPhysics.transform;
            _startPoint = _chestEntityTransform.position.y;
            _chestEntityPhysics.SetCollidersEnabled(false);
        }

#endregion

    }
}
