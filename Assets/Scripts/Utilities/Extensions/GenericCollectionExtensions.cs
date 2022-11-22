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
    }
}