using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Effects;
using Game.Characters.Enums;
using Game.Characters.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.States.Managers;


namespace Game.Characters.States
{
    internal class PersueState : BaseState
    {
        [SerializeField] private bool beTakenByPlayerBeforeTerminate;
        public override StateEntityType StateEntityType { get => StateEntityType.Persue; }

        public override void Run()
        {
            _thisTransform.LookAt(_playerTransform.position + new Vector3(0, _halfPlayerHeight, 0));
            _thisTransform.position += transform.forward * speed * Time.deltaTime;
        }

        private protected override void BeforeTerminate()
        {
            if (beTakenByPlayerBeforeTerminate)
            {
                _player.Take(_chestEntity);
            }
        }

#region Kernel Entity

        [ConstructField]
        private Player _player;

        [ConstructField]
        private IChestEntity _chestEntity;

        private Transform _playerTransform;
        private Transform _thisTransform;
        private float _halfPlayerHeight;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _playerTransform = _player.transform;
            _thisTransform = kernel.GetInjection<MovingAgent>().transform;
            _halfPlayerHeight = _playerTransform.localScale.y / 2;
        }

#endregion
    }
}
