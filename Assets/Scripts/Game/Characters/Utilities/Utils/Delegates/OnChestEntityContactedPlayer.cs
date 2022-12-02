using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Enums;

namespace Game.Characters.Utilities.Utils.Delegates
{
    internal delegate void OnChestEntityContactedPlayer(Transform chestEntity, OwnerType ownerType);
}