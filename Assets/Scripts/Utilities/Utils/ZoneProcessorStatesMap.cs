using System;
using Game.Characters.Handlers;

namespace Utilities.Utils
{
    [Serializable]
    internal class ZoneProcessorStatesMap : GenericKeyValuePair<DistanceToSubjectZoneProcessor, ZoneStatesOnEnterAndExit>
    {
        internal ZoneProcessorStatesMap(DistanceToSubjectZoneProcessor inputKey, ZoneStatesOnEnterAndExit inputValue) : base(inputKey, inputValue) { }
    }
}