using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Characters.Enums
{
    [Flags]
    internal enum OwnerTypeFlags
    {
        Creature = 1,
        Chest = 2,
        Runtime = 4,
        Other = 8,
    }
}
