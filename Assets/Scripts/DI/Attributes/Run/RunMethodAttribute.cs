using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using DI.Kernel.Interfaces;

namespace DI.Attributes.Run
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class RunMethodAttribute : Attribute
    {

    }
}
