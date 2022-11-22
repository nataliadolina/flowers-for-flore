using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IBody : ITransform
    {
        void SetCollidersEnabled(bool enabled);

        void SetRigidbodiesEnabled(bool enabled);

    }
}
