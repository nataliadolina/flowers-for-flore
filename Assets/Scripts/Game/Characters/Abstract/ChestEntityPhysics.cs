﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;
using Game.Characters.Effects;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;

namespace Game.Characters.Abstract
{
    internal abstract class ChestEntityPhysics : MonoBehaviour, IKernelEntity
    {
        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        protected Animator _animator;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _colliders = GetComponentsInChildren<Collider>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        internal void SetCollidersEnabled(bool value)
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

        internal void SetRigidbodiesEnabled(bool value)
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
