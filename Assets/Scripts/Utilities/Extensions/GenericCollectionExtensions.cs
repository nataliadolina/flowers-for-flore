using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Utilities.Extensions
{
    internal static class GenericCollectionExtensions
    {
        internal static Array ToArray(this IEnumerable source, Type type)
        {
            var sourceCopy = (IEnumerable<object>)source;
            var srcArray = sourceCopy.ToArray();

            Array filledArray = Array.CreateInstance(type, srcArray.Length);
            Array.Copy(srcArray, filledArray, srcArray.Length);
            return filledArray;
        }

        internal static Array JoinToArray(Array source, Type type, params IEnumerable[] newValues)
        {
            IList arrayList = new ArrayList();
            arrayList.Add(source);
            foreach (object value in newValues)
            {
                arrayList.Add(value);
            }
            return arrayList.ToArray(type);
        }

        internal static IList AddRange(this IList list, IEnumerable itemsToAdd)
        {
            foreach (var item in itemsToAdd)
            {
                list.Add(item);
            }
            return list;
        }

        internal static List<TValue> GetValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> hashMap)
        {
            List<TValue> values = new List<TValue>();
            foreach (var keyValuePair in hashMap)
            {
                values.Add(keyValuePair.Value);
            }
            return values;
        }

        internal static List<TKey> GetKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> hashMap)
        {
            List<TKey> keys = new List<TKey>();
            foreach (var keyValuePair in hashMap)
            {
                keys.Add(keyValuePair.Key);
            }
            return keys;
        }
    }
}