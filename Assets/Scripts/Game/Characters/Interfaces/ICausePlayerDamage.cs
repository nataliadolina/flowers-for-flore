using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Characters.Interfaces
{
    internal interface ICausePlayerDamage
    {
        event Action<float> onCausedPlayerDamage;
    }
}