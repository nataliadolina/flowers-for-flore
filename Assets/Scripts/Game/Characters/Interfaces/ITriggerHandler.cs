using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface ICollisionDetector
    {
        void ColliderSetEnabled(bool isEnabled);
    }
}