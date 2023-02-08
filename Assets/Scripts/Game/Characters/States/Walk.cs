using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Characters.States.Abstract;
using Game.Characters.Enums;
using DI.Attributes.Run;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.States.Movements;

namespace Game.Characters.States {

    [Register]
    internal class Walk : BaseState
    {
        [SerializeField] private MovingSystem movingSystem;
        [SerializeField] private TypeMove typeMove;
        private Transform targetPoint = null;

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
            if (_chestEntityTransform.position == targetPoint.position)
            {
                targetPoint = movingSystem.GetNextPoint(typeMove);
            }
            MoveToTheNextPoint();
        }

        private void MoveToTheNextPoint()
        {
            _chestEntityTransform.LookAt(targetPoint);
            _chestEntityTransform.position = Vector3.MoveTowards(_chestEntityTransform.position, targetPoint.position, speed * Time.fixedDeltaTime);
        }

        public override void OnStartState()
        {
            Debug.Log("Walk Set detectors enabled");
            _body.SetCollisionDetectorsEnabled(true);
            _body.SetRigidbodiesEnabled(false);
        }

#region Kernel Entity

        private IBody _body;

        private Transform _chestEntityTransform;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _body = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity);
            _chestEntityTransform = _body.Transform;
        }

#endregion

    }
}
