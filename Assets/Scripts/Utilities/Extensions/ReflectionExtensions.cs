using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Utilities.Extensions
{
    internal static class ReflectionExtensions
    {
        private const string IENUMERABLE_TYPE_NAME = nameof(IEnumerable);
        private static readonly Type IEnumerableType = typeof(IEnumerable<>);

        internal static bool IsMultiple(this Type type)
        {
            return type.IsArray || type.IsGenericIEnumerable();
        }

        private static bool IsGenericIEnumerable(this Type type)
        {
            return type.IsGenericType && type.GetInterfaces().Any(i => i.GetType() == IEnumerableType || i.Name == IENUMERABLE_TYPE_NAME); 
        }
    }
}