using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Characters.Enums
{
    [Flags]
    internal enum OwnerTypeFlags
    {
        Player,
        Monster,
        Flower,
        ChestEntity,
        Chest,
        Runtime
    }
}
