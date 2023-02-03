using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IStateSwitcher
    {
        public bool NeedToSwitchState { set; }
    }
}
