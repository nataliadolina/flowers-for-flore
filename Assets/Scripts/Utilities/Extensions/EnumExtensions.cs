using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace Utilities.Extensions
{
    internal static class EnumExtensions
    {
        internal static bool HasValue(this Enum thisEnum, string value)
        {
            List<string> names = Enum.GetNames(thisEnum.GetType()).ToList<string>();
            return names.Contains(value);
        }

    }
}
