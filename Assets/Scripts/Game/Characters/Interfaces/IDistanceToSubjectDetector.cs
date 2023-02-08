using System;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IDistanceToSubjectDetector : ICollisionDetector, IAimType, IMultipleOwnerType
    {
        /// <summary>
        /// Event that is called when distance to the aim GameObject changes
        /// float distance to aim 
        /// </summary>
        public event Action<float> onDistanceToSubjectChange;

        public Transform AimTransform { get; }
    }
}
