using System;

namespace Game.Characters.Interfaces
{
    internal interface IDistanceToSubjectDetector : ICollisionDetector, IAimType, IMultipleOwnerType
    {
        public event Action<float> onDistanceToSubjectChange;
    }
}
