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

        void Open();

        void Destroy();

        bool IsSelected { get; set; }
        bool IsVisible { get; set; }
    }
}