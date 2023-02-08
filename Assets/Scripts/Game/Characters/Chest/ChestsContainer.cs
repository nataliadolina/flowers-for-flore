using System;
using System.Collections.Generic;
using UnityEngine;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters.Interfaces;
using Game.Characters.Utilities.Utils;
using Utilities.Extensions;
using System.Linq;

namespace Game.Characters.Chest
{
    [Register(typeof(IChestsContainer))]
    internal class ChestsContainer : MonoBehaviour, IKernelEntity, IChestsContainer
    {
        public event Action<bool> onIsThereAnyOpenableChestsChanged;

        private Dictionary<IChest, DistanceToPlayerArgs> _chestAnimatorDistanceToPlayerMap = new Dictionary<IChest, DistanceToPlayerArgs>();
        private List<DistanceToPlayerArgs> _openableChests = new List<DistanceToPlayerArgs>();

        private bool _isThereAnyOpenableChests = false;
        private bool IsThereAnyOpenableChests 
        { 
            get => _isThereAnyOpenableChests;
            set
            {
                if (value != _isThereAnyOpenableChests)
                {
                    _isThereAnyOpenableChests = value;
                    onIsThereAnyOpenableChestsChanged?.Invoke(value);
                }
                
            }
        }

        private void UpdateOpenableChests()
        {
            _openableChests = _chestAnimatorDistanceToPlayerMap.Where(x => x.Value.CanBeOpened == true).GetValues();
        }

        private void OpenChest(DistanceToPlayerArgs distanceToPlayerArgs)
        {
            IChest chest = distanceToPlayerArgs.Chest;

            chest.onPlayerEnteredChestZone -= AnyChestDistanceToPlayerChange;
            _chestAnimatorDistanceToPlayerMap.Remove(chest);
            _openableChests.Remove(distanceToPlayerArgs);
            UpdateOpenableChests();

            chest.Open();
            
            IsThereAnyOpenableChests = _openableChests.Count > 0;
        }

        private void AnyChestDistanceToPlayerChange(DistanceToPlayerArgs distanceToPlayerArgs)
        {
            bool isExistingChest = false;
            IChest currentChest = distanceToPlayerArgs.Chest;
            foreach (var chest in _chestAnimatorDistanceToPlayerMap.Keys)
            {
                if (chest.Equals(currentChest))
                {
                    _chestAnimatorDistanceToPlayerMap[chest] = distanceToPlayerArgs;
                    isExistingChest = true;
                    break;
                }
            }

            if (!isExistingChest)
            {
                _chestAnimatorDistanceToPlayerMap.Add(currentChest, distanceToPlayerArgs);
            }

            UpdateOpenableChests();
            IsThereAnyOpenableChests = _openableChests.Count > 0;
        }

        private void PlayerExitedAnyChestZone(IChest chest)
        {
            _chestAnimatorDistanceToPlayerMap.Remove(chest);
            UpdateOpenableChests();
            IsThereAnyOpenableChests = _openableChests.Count > 0;
        }

        public void OpenSelectedChest()
        {
            if (!IsThereAnyOpenableChests)
            {
                return;
            }

            UpdateOpenableChests();
            float minDistanceToPlayer = _openableChests.Min(x => x.DistanceToPlayer);
            var chestToOpenInfo = _openableChests.Where(x => x.DistanceToPlayer == minDistanceToPlayer).FirstOrDefault();
            OpenChest(chestToOpenInfo);
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubstriptions();
        }

#endregion

#region Kernel entity

        [ConstructField(KernelTypeOwner.LogicScene)]
        private IChest[] chests;

        [ConstructMethod(KernelTypeOwner.LogicScene)]
        private void OnConstruct(IKernel kernel)
        {
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            foreach (var chest in chests)
            {
                chest.onPlayerEnteredChestZone += AnyChestDistanceToPlayerChange;
                chest.onPlayerExitedChestZone += PlayerExitedAnyChestZone;
            }
        }

        private void ClearSubstriptions()
        {
            foreach (var chest in chests)
            {
                chest.onPlayerEnteredChestZone -= AnyChestDistanceToPlayerChange;
                chest.onPlayerExitedChestZone -= PlayerExitedAnyChestZone;
            }
        }

#endregion
    }
}