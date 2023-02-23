using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using Utilities;
using System;

namespace Game.Characters.States
{
    internal class PersueScanningAreaState : BaseState
    {
        [SerializeField] private float degreesSpeed;
        [SerializeField] private float degreesThreshold;
        [SerializeField] private int startDirection;

        private int _currentDirection;
        private float _currentDegreesOffset = 0;
        private float _halfDegreesThreshold;

        public override StateEntityType StateEntityType { get => StateEntityType.Persue; }
        private float _degreesPerFrame;

        public override void Run()
        {
            float rotateDegree = _degreesPerFrame * _currentDirection;
            _currentDegreesOffset += rotateDegree;

            if (_currentDegreesOffset <= -_halfDegreesThreshold || _currentDegreesOffset >= _halfDegreesThreshold)
            {
                _currentDirection *= -1;
            }

            _entityTransform.Rotate(new Vector3(0, rotateDegree, 0));
        }

#region KernelEntity

        private Transform _entityTransform;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _halfDegreesThreshold = degreesThreshold / 2;
            _currentDirection = startDirection;
            _degreesPerFrame = degreesSpeed * GameParams.UPDATE_RATE;
            _entityTransform = kernel.GetInjection<IBody>().Transform;
        }

#endregion
    }
}
