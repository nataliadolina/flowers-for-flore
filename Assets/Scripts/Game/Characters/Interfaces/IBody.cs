using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IBody : ITransform, IRigidbody, IOwnerType
    {
        void SetCollisionDetectorsEnabled(bool enabled);

        void SetRigidbodiesEnabled(bool enabled);

    }
}
