using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;

namespace Game.Characters.Utilities.Utils
{
    internal struct DistanceToPlayerArgs
    {
        private Transform _chestTransform;
        private Transform _playerTransform;
        
        internal IChest Chest;

        internal float DistanceToPlayer { get => Vector3.Distance(_playerTransform.position, _chestTransform.position); }
        internal bool CanBeOpened { get => Chest.CanBeOpened; }

        internal DistanceToPlayerArgs(Transform chestTransform, Transform playerTransform, IChest chest)
        {
            _chestTransform = chestTransform;
            _playerTransform = playerTransform;
            Chest = chest;
        }
    }
}