using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IChestsContainer
    {
        event Action<bool> onIsThereAnyOpenableChestsChanged;

        void OpenSelectedChest();
    }
}
