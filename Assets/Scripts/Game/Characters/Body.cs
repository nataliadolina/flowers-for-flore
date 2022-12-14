using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using Game.Characters.Enums;

namespace Game.Characters
{
    /// <summary>
    /// Operates object physics using Trannsform, Rigidbody and Collider components
    /// </summary>
    /// 
    [Register(typeof(IBody))]
    internal class Body : MonoBehaviour, IBody, IKernelEntity
    {
        [SerializeField]
        private OwnerType _ownerType;

        private Rigidbody[] _rigidbodies;

        public Transform Transform { get; private set; }

#region IOwnerType

        public OwnerType OwnerType { get => _ownerType; }

#endregion

        public void SetCollisionDetectorsEnabled(bool value)
        {
            if (_collisionDetectors == null)
            {
                return;
            }

            foreach (var detector in _collisionDetectors)
            {
                detector.ColliderSetEnabled(value);
            }
        }

        public void SetRigidbodiesEnabled(bool value)
        {
            if (_rigidbodies == null)
            {
                return;
            }

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = !value;
            }
        }

#region Kernel Entity

        [ConstructField]
        private ICollisionDetector[] _collisionDetectors;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            Transform = transform;
        }

#endregion

    }
}
