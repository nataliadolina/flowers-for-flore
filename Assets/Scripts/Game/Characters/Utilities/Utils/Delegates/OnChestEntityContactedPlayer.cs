using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Enums;
using Game.Characters.Interfaces;

namespace Game.Characters.Utilities.Utils.Delegates
{
    internal delegate void OnChestEntityContactedPlayer(IBody chestEntity, OwnerType ownerType, StateEntityType stateEntityType);
}