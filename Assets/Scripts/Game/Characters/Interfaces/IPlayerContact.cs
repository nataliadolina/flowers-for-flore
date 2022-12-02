using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Utilities.Utils.Delegates;

namespace Game.Characters.Interfaces
{
    internal interface IPlayerContact
    {
        event OnChestEntityContactedPlayer onChestEntityContancedPlayer;
    }
}
