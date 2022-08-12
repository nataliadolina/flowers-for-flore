using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities.Utils
{
    [Serializable]
    internal abstract class SerialiaziableKeyValueMap<TKey, TValue>
    {
        [SerializeField]
        internal TKey Key;

        [SerializeField]
        internal TValue Value;

        internal SerialiaziableKeyValueMap(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
