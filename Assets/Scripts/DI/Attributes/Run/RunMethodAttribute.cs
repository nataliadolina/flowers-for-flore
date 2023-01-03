using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using DI.Kernel;

namespace DI.Attributes.Run
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class RunMethodAttribute : Attribute
    {
        private IKernel _kernel = null;
        internal RunMethodAttribute(KernelTypeOwner ownerType = KernelTypeOwner.Default)
        {
            if (ownerType != KernelTypeOwner.Default)
            {
                _kernel = KernelManager.Instance.KernelOwnerMap[ownerType][0];
            }
        }

        internal object[] GetParametres(IKernel kernel)
        {
            if (_kernel != null)
            {
                return new object[] { _kernel };
            }
            return new object[] { kernel };
        }
    }
}
