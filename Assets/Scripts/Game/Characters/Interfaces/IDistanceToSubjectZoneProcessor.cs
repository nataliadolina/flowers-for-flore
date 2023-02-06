using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Utilities.Utils;
using System;

namespace Game.Characters.Interfaces
{
    internal interface IDistanceToSubjectZoneProcessor : IOwnerType, IAimType
    {
        public event Action<float> onAimEnterZone;
        public event Action onAimExitZone;

        public bool IsSubjectInsideZone { get; }
    }
}