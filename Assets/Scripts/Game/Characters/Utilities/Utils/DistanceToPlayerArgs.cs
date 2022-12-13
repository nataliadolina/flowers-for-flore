using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;

namespace Game.Characters.Utilities.Utils
{
    internal struct DistanceToPlayerArgs
    {
        internal float DistanceToPlayer;
        internal IChest Chest;
        internal bool CanBeOpened;

        internal DistanceToPlayerArgs(float distanceToPlayer, IChest chest, bool canBeOpened)
        {
            DistanceToPlayer = distanceToPlayer;
            Chest = chest;
            CanBeOpened = canBeOpened;
        }
    }
}