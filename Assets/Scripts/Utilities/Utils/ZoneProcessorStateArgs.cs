using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Handlers;
using Game.Characters.Enums;
using System;

namespace Utilities.Utils
{
    [Serializable]
    internal struct ZoneProcessorStateArgs
    {
        [SerializeField]
        private int priority;

        [SerializeField]
        private DistanceToSubjectZoneProcessor distanceToSubjectZoneProcessor;

        [SerializeField]
        private StateEntityType stateEntityTypeOnEnterZone;

        internal int Priority => priority;

        internal DistanceToSubjectZoneProcessor DistanceToSubjectZoneProcessor => distanceToSubjectZoneProcessor;
        
        internal StateEntityType StateEntityTypeOnEnterZone => stateEntityTypeOnEnterZone;

        internal ZoneProcessorStateArgs(int priorityInput, DistanceToSubjectZoneProcessor distanceToSubjectZoneProcessorInput, StateEntityType stateEntityTypeOnEnterZoneInput, StateEntityType stateEntityTypeOnExitZoneInput)
        {
            priority = priorityInput;
            distanceToSubjectZoneProcessor = distanceToSubjectZoneProcessorInput;
            stateEntityTypeOnEnterZone = stateEntityTypeOnEnterZoneInput;
        }
    }
}
