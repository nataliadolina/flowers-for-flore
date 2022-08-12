using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Interfaces;

namespace DI.Attributes.Construct
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ConstructMethodAttribute : Attribute
    {
        private Type _kernelType;
        internal IKernel Kernel;
        internal ConstructMethodAttribute(Type kernelType = null)
        {
            _kernelType = kernelType;
        }
    }
}