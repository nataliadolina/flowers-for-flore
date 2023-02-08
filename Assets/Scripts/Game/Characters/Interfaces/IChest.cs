using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Utilities.Utils;

namespace Game.Characters.Interfaces
{
    internal interface IChest: ITransform
    {
        public event Action onOpened;
        public event Action<DistanceToPlayerArgs> onPlayerEnteredChestZone;
        public event Action<IChest> onPlayerExitedChestZone;

        public bool CanBeOpened { get; }

        public void Open();
        public void Destroy();
    }
}