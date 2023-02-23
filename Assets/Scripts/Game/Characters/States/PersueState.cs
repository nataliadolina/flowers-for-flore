﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Effects;
using Game.Characters.Enums;
using Game.Characters.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using Game.Characters.States.Managers;
using Game.Characters.Player;
using DI.Kernel.Enums;
using Game.Characters.Abstract;

namespace Game.Characters.States
{
    [Register]
    internal class PersueState : BaseState
    {
        [SerializeField] private float speed;

        [SerializeField] private StateEntityType stateEntityType = StateEntityType.Persue;
        public override StateEntityType StateEntityType { get => stateEntityType; }

        public override void Run()
        {
            _thisTransform.LookAt(_playerTransform.position + new Vector3(0, _halfPlayerHeight, 0));
            _thisTransform.position += transform.forward * speed * Time.deltaTime;
        }

        public override void OnStartState()
        {
            _chestEntitybody.SetCollisionDetectorsEnabled(true);
        }

#region Kernel Entity

        private IBody _chestEntitybody;

        private Transform _playerTransform;
        private Transform _thisTransform;
        private float _halfPlayerHeight;

        [RunMethod]
        private void OnConstruct(IKernel kernel)
        {
            _chestEntitybody = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.Creature);
            _thisTransform = _chestEntitybody.Transform;
        }

        [RunMethod(KernelTypeOwner.Player)]
        private void OnRun(IKernel kernel)
        {
            _playerTransform = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.Player).Transform;
            _halfPlayerHeight = _playerTransform.localScale.y / 2;
        }

#endregion
    }
}
