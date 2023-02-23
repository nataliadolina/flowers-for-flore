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
    [Register(typeof(IAppearState))]
    internal class FloatUpState : BaseState, IAppearState
    {
        public event Action onAppearStateTerminated;

        [SerializeField]
        private float speed;

        [SerializeField] private float height = 1f;

        public override StateEntityType StateEntityType { get => StateEntityType.Appear; }

        public override void Run()
        {
            float delta = _creatureTransform.position.y - _startPoint;
            if (delta < height)
            {
                float translateSpeed = speed;
                if (delta > 0.5f * height)
                    translateSpeed = 2 * speed;
                _creatureTransform.Translate(Vector3.up * translateSpeed * Time.deltaTime);
            }
            else
            {
                Terminate();
            }
        }

        public override void BeforeTerminate()
        {
            onAppearStateTerminated?.Invoke();
        }


#region Kernel Entity

        private Transform _creatureTransform;

        private float _startPoint;

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            IBody creatureBody = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.Creature);
            _creatureTransform = creatureBody.Transform;
            _startPoint = _creatureTransform.position.y;
        }

#endregion

    }
}
