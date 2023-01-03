using System;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IFlowerTriggerEnterHandler
    {
        event Action<IBody> onFlowersContainerContact;
    }
}