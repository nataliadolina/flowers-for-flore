using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;
using Game.Characters.Effects;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;

namespace Game.Characters.Abstract
{
    [Register]
    [Register(typeof(IBody))]
    internal abstract class ChestEntityPhysics : MonoBehaviour, IBody, IKernelEntity
    {
        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        protected Animator _animator;
        
        public Transform Transform { get; private set; }

#region MonoBehaviour

        private void Awake()
        {
            _colliders = GetComponentsInChildren<Collider>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            Transform = transform;
        }

#endregion

        public void SetCollidersEnabled(bool value)
        {
            if (_colliders == null)
            {
                return;
            }

            foreach (Collider collider in _colliders)
            {
                collider.enabled = value;
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

        internal void DisableRigidBodiesAndColliders()
        {
            SetCollidersEnabled(false);
            SetRigidbodiesEnabled(false);
        }

        internal void EnableRigidBodiesAndColliders()
        {
            SetCollidersEnabled(true);
            SetCollidersEnabled(true);
        }
    }
}
