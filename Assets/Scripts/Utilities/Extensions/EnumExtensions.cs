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

            if (enumSelectedValue == "-1")
            {
                return true;
            }

            if (enumSelectedValue == "-2")
            {
                return false;
            }
            
            return enumSelectedValue == value;
        }

    }
}
