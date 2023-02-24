using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IPoolObject
    {
        public void Activate();
        public void ResetPoolObject();
    }
}