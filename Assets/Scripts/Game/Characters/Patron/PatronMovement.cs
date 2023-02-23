using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Effects;
using System;
using Game.Characters.Enums;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters.Player;
using Game.Characters.Interfaces;
using Utilities;
using Game.Utilities.Behaviours;
using Utilities.Utils;
using System.Collections.Generic;

namespace Game.Characters.Patron
{
    [Register]
    internal class PatronMovement : MultiThreadedUpdateMonoBehaviour, IKernelEntity
    {
        [SerializeField]
        private float speed;

        private Transform _thisTransform;

        private void Run()
        {
            _thisTransform.Translate(_thisTransform.forward * speed * GameParams.UPDATE_RATE);
        }

        private void SetDirection()
        {
            _thisTransform.LookAt(_playerPosition);
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

        private Vector3 _playerPosition;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _thisTransform = transform;
            SetDirection();
        }

        [RunMethod(KernelTypeOwner.Player)]
        private void OnRun(IKernel kernel)
        {
            _playerPosition = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.Player).Transform.position;
            StartInternal();
        }

#endregion
    }
}
