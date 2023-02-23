using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Attributes.Run;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.Utilities.Utils;
using Game.Utilities.Behaviours;
using System;
using Utilities.Utils;

namespace Game.Characters.Handlers
{
    [Register]
    [Register(typeof(IDistanceToSubjectDetector))]
    internal class DistanceToSubjectDetector : MultiThreadedUpdateMonoBehaviour, IDistanceToSubjectDetector, IKernelEntity
    {
        public event Action<float> onDistanceToSubjectChange;

        [SerializeField]
        private OwnerTypeFlags ownerTypes;
        [SerializeField]
        private OwnerType aimType;

        private protected Transform _subjectTransform;
        private protected Transform _thisTransform;

        private float _currentDistance;

        private bool _isDetectionAreaActive = true;

        public OwnerType AimType { get => aimType; }

        public OwnerTypeFlags OwnerTypes { get => ownerTypes; }

        public Transform AimTransform { get => _subjectTransform; }

        private float CurrentDistance { get => _currentDistance;
            set
            {
                _currentDistance = value;
                onDistanceToSubjectChange?.Invoke(value);
            } 
        }

        private void UpdateDistance()
        {
            if (!_isDetectionAreaActive)
            {
                return;
            }

            CurrentDistance = Vector3.Distance(_subjectTransform.position, _thisTransform.position);
        }

        private protected override void SetUpUpdateSettings()
        {
            _updateThreads = new List<InvokeRepeatingSettings> {
                new InvokeRepeatingSettings(nameof(UpdateDistance))
            };
        }

#region ICollision Detector

        public void ColliderSetEnabled(bool isEnabled)
        {
            _isDetectionAreaActive = isEnabled;
        }

#endregion

        [RunMethod(KernelTypeOwner.LogicScene)]
        private void OnRun(IKernel kernel)
        {
            _thisTransform = transform;
            _subjectTransform = kernel.GetInjection<IBody>(x => x.OwnerType == aimType).Transform;
            StartInternal();
        }
    }
}