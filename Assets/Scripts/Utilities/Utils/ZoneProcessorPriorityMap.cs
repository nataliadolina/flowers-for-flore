using Game.Characters.Handlers;
using System;

namespace Utilities.Utils
{
    [Serializable]
    internal sealed class ZoneProcessorPriorityMap : GenericKeyValuePair<int, DistanceToSubjectZoneProcessor>
    {
        internal ZoneProcessorPriorityMap(int inputKey, DistanceToSubjectZoneProcessor inputValue) : base(inputKey, inputValue) { }
    }
}
