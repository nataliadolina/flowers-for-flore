using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IChest: ITransform
    {
        void Open();
        void Destroy();
        bool IsSelected { get; set; }
        bool IsVisible { get; set; }
    }
}