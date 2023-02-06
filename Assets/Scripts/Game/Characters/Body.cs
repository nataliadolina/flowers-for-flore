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
    /// Operates object physics using Transform, Rigidbody, Collider and DistanceToSubjectDetector components
    /// </summary>
    /// 
    [Register(typeof(IBody))]
    internal class Body : MonoBehaviour, IBody, IKernelEntity
    {
        [SerializeField]
        private OwnerType _ownerType;

        private Rigidbody[] _rigidbodies;

        private Rigidbody _mainRigidBody;

#region IRigidBody

        public Rigidbody Rigidbody { get => _mainRigidBody; }

#endregion

#region ITransform

        public Transform Transform { get => transform; }

#endregion

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
            _mainRigidBody = GetComponent<Rigidbody>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

#endregion

    }
}
