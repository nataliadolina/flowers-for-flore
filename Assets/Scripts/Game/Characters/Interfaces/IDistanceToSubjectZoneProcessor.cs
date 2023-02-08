using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Utilities.Utils;
using System;

namespace Game.Characters.Interfaces
{
    internal interface IDistanceToSubjectZoneProcessor : IOwnerType, IAimType
    {
        /// <summary>
        /// Passes aim GameObject Transform when it enteres the zone
        /// </summary>
        public event Action<Transform> onAimEnterZone;
        public event Action onAimExitZone;

        public bool IsSubjectInsideZone { get; }
    }
}