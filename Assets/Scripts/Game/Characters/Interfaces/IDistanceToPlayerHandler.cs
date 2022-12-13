using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Utilities.Utils;
using System;

namespace Game.Characters.Interfaces
{
    internal interface IDistanceToPlayerHandler
    {
        event Action<DistanceToPlayerArgs> onDistanceToPlayerChange;
    }
}