using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Enums;

namespace Utilities.Utils
{
    internal struct ZoneStatesOnEnterAndExit
    {
        [SerializeField]
        private StateEntityType stateOnEnter;

        [SerializeField]
        private StateEntityType stateOnExit;

        internal StateEntityType StateOnEnter => stateOnEnter;
        internal StateEntityType StateOnExit => stateOnExit;

        internal ZoneStatesOnEnterAndExit(StateEntityType stateOnEnterInput, StateEntityType stateOnExitInput)
        {
            stateOnEnter = stateOnEnterInput;
            stateOnExit = stateOnExitInput;
        }
    }
}