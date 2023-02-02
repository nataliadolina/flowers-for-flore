using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Utilities.Utils;

namespace Game.Characters.Interfaces
{
    internal interface IChest: ITransform
    {
        event Action onOpened;
        public event Action<DistanceToPlayerArgs> onPlayerEnteredChestZone;

        void Open();

        void Destroy();

        bool IsSelected { get; set; }
        bool IsVisible { get; set; }
    }
}