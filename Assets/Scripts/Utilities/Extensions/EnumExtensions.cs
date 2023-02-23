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
            string enumSelectedValue = thisEnum.ToString();
            return enumSelectedValue.Contains(value);
        }

    }
}
