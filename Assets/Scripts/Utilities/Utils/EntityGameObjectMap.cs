using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Labirint.Enums;
using System;

namespace Utilities.Utils
{
    [Serializable]
    internal class EntityObjectMap : SerialiaziableKeyValueMap<Entity, GameObject> {
        internal EntityObjectMap(Entity key, GameObject value) : base(key, value) { }
    }
}
