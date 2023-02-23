using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities.Utils
{
    [Serializable]
    internal class GenericKeyValuePair<TKey, TValue>
    {
        [SerializeField]
        private TKey key;
        [SerializeField]
        private TValue value;

        internal TKey Key => key;
        internal TValue Value => value;

        internal GenericKeyValuePair(TKey inputKey, TValue inputValue)
        {
            key = inputKey;
            value = inputValue;
        }
    }
}