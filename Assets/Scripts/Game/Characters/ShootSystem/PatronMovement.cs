using UnityEngine;
using Game.Characters.Enums;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters.Interfaces;
using Utilities;
using Game.Utilities.Behaviours;
using Utilities.Utils;
using System.Collections.Generic;

namespace Game.Characters.ShootSystem
{
    [Register]
    internal class PatronMovement : MultiThreadedUpdateMonoBehaviour, IPoolObject, IKernelEntity
    {
        [SerializeField]
        private float speed;

        private Transform _thisTransform;
        private Vector3 _startPosition;

        private void Run()
        {
            _thisTransform.position += _thisTransform.forward * speed * GameParams.UPDATE_RATE;
        }

        public void Activate()
        {
            SetDirection();
            gameObject.SetActive(true);
            StartUpdate();
        }

        public void ResetPoolObject()
        {
            _thisTransform.position = _startPosition;
            gameObject.SetActive(false);
            StopUpdate();
        }

        private void SetDirection()
        {
            Vector3 playerHeadPosition = _playerBodyTransform.position + new Vector3(0, 0.5f, 0);
            _thisTransform.LookAt(playerHeadPosition);
        }

#region Update

        private protected override void SetUpUpdateSettings()
        {
            _updateThreads = new List<InvokeRepeatingSettings> {
                new InvokeRepeatingSettings(nameof(Run))
            };
        }

#endregion

#region Kernel Entity

        private Transform _playerBodyTransform;
 
        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _thisTransform = transform;
            _startPosition = transform.position;
        }

        [RunMethod(KernelTypeOwner.Player)]
        private void OnRun(IKernel kernel)
        {
            _playerBodyTransform = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.Player).Transform;
            SetUpUpdateSettings();
            gameObject.SetActive(false);
        }

#endregion
    }
}
