using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;

namespace Game.Characters.Handlers
{
    [Register(typeof(IDistanceToSubjectZoneProcessor))]
    internal class DistanceToSubjectZoneProcessor : MonoBehaviour, IDistanceToSubjectZoneProcessor, IKernelEntity
    {
        public event Action<float> onAimEnterZone;
        public event Action onAimExitZone;

        [SerializeField] private float maxVisibleDistance;
        [SerializeField] private OwnerType ownerType;
        [SerializeField] private OwnerType aimType;

        private bool _isSubjectInsideZone = false;
        private float _currentDistance;

        public bool IsSubjectInsideZone
        {
            get => _isSubjectInsideZone;
            set
            {
                if (_isSubjectInsideZone != value)
                {
                    if (!value)
                    {
                        onAimExitZone?.Invoke();
                    }
                    else if (value)
                    {
                        onAimEnterZone?.Invoke(_currentDistance);
                    }
                    _isSubjectInsideZone = value;
                }
            }
        }

#region Owner Type

        public OwnerType OwnerType { get=> ownerType; }

#endregion

#region Aim Type

        public OwnerType AimType { get => aimType; }

#endregion

        private void SetIsSubjectInsideZone(float distanceToAim)
        {
            _currentDistance = distanceToAim;
            IsSubjectInsideZone = distanceToAim <= maxVisibleDistance ? true : false;
        }

#region KernelEntity

        [ConstructField]
        private IDistanceToSubjectDetector _distanceToSubjectDetector;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _distanceToSubjectDetector = kernel.GetInjection<IDistanceToSubjectDetector>(x => x.AimType == aimType && x.OwnerTypes.HasFlag(ownerType));
        }

        private void SetSubscriptions()
        {
            _distanceToSubjectDetector.onDistanceToSubjectChange += SetIsSubjectInsideZone;
        }

#endregion

#if UNITY_EDITOR

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, maxVisibleDistance);
        }

#endif
    }
}
