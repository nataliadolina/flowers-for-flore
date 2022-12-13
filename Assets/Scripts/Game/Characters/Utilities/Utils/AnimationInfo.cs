using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Utilities.Utils
{
    internal struct AnimationHandlerInfo
    {
        internal string Name;
        internal int Index;
        internal float Duration;

        internal AnimationHandlerInfo(string name, int index, float duration)
        {
            Name = name;
            Index = index;
            Duration = duration;
        }
    }
}