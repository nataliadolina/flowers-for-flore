using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;

namespace Game.Characters.Effects
{
    internal class Particles : MonoBehaviour, IKernelEntity
    {
        private bool _isActivated;
        internal bool IsActivated
        {
            get => _isActivated;
            set
            {
                _isActivated = value;
                if (value)
                {
                    Play();
                }
                else
                {
                    Stop();
                }
            }
        }

        private void Play()
        {
            if (_particles != null)
            {
                foreach (ParticleSystem p in _particles)
                {
                    p.Play();
                }
            }
        }

        private void Stop()
        {
            foreach (ParticleSystem p in _particles)
            {
                p.Stop();
            }
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            Destroy(gameObject);
        }

#endregion

#region Kernel entity

        private ParticleSystem[] _particles;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _particles = GetComponentsInChildren<ParticleSystem>();
        }

#endregion
    }
}
