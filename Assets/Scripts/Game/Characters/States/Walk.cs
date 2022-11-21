using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Characters.States.Abstract;
using Game.Characters.Enums;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using Game.Characters.Abstract;
using Game.Characters.States.Movements;

namespace Game.Characters.States {

    [Register]
    internal class Walk : BaseState
    {
        [SerializeField] private MovingSystem movingSystem;
        [SerializeField] private TypeMove typeMove;
        private Transform targetPoint = null;
        private Transform _monsterTransform;

        public override StateEntityType StateEntityType { get => StateEntityType.Walk; }

#region MonoBehaviour

        private void Start()
        {
            movingSystem.CreateWay();
            targetPoint = movingSystem.GetStartPoint();
        }

#endregion

        public override void Run()
        {
            if (_monsterTransform.position == targetPoint.position)
            {
                targetPoint = movingSystem.GetNextPoint(typeMove);
            }
            MoveToTheNextPoint();
        }

        private void MoveToTheNextPoint()
        {
            _monsterTransform.LookAt(targetPoint);
            _monsterTransform.position = Vector3.MoveTowards(_monsterTransform.position, targetPoint.position, speed * Time.fixedDeltaTime);
        }

#region Kernel Entity

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _monsterTransform = kernel.GetInjection<ChestEntityPhysics>().transform;
        }

#endregion

    }
}
