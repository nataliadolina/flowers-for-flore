using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using System.Reflection;
using DI.Kernel;

namespace DI.Attributes.Construct
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class ConstructFieldAttribute : Attribute
    {
        private const string NONUMERIC_METHOD_NAME = nameof(IKernel.GetReflectionInjection);
        private const string NUMERIC_METHOD_NAME = nameof(IKernel.GetReflectionInjections);

        private IKernel _kernel;
        
        public ConstructFieldAttribute(KernelTypeOwner ownerType = KernelTypeOwner.Default)
        {
            if (ownerType != KernelTypeOwner.Default)
            {
                _kernel = KernelManager.Instance.KernelOwnerMap[ownerType][0];
            }
        }

        internal object FindInstance(IKernel kernel, Type type)
        {
            MethodInfo method = typeof(IKernel).GetMethod(NONUMERIC_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance)
                                ?? throw new NullReferenceException($"Can't find method \'{NONUMERIC_METHOD_NAME}\' for kernel \'{kernel.GetType().Name}\'");
            var invokeArgs = new object[1];
            invokeArgs[0] = type;
            var finalKernel = _kernel ?? kernel;
            return method.Invoke(finalKernel, invokeArgs);
        }

        internal IEnumerable<object> FindInstances(IKernel kernel, Type type)
        {
            MethodInfo method = typeof(IKernel).GetMethod(NUMERIC_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance)
                                ?? throw new NullReferenceException($"Can't find method \'{NUMERIC_METHOD_NAME}\' for kernel \'{kernel.GetType().Name}\'");

            var invokeArgs = new object[1];
            invokeArgs[0] = type;
            var finalKernel = _kernel ?? kernel;
            return (IEnumerable<object>)method.Invoke(finalKernel, invokeArgs);
        }
    }
}