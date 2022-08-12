using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Interfaces;
using System.Reflection;

namespace DI.Attributes.Construct
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class ConstructFieldAttribute : Attribute
    {
        private const string NONUMERIC_METHOD_NAME = nameof(IKernel.GetReflectionInjection);
        private const string NUMERIC_METHOD_NAME = nameof(IKernel.GetReflectionInjections);

        internal object FindInstance(IKernel kernel, Type type)
        {
            MethodInfo method = typeof(IKernel).GetMethod(NONUMERIC_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance)
                                ?? throw new NullReferenceException($"Can't find method \'{NONUMERIC_METHOD_NAME}\' for kernel \'{kernel.GetType().Name}\'");
            var invokeArgs = new object[1];
            invokeArgs[0] = type;
            return method.Invoke(kernel, invokeArgs);
        }

        internal IEnumerable<object> FindInstances(IKernel kernel, Type type)
        {
            MethodInfo method = typeof(IKernel).GetMethod(NUMERIC_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance)
                                ?? throw new NullReferenceException($"Can't find method \'{NUMERIC_METHOD_NAME}\' for kernel \'{kernel.GetType().Name}\'");

            var invokeArgs = new object[1];
            invokeArgs[0] = type;
            return (IEnumerable<object>)method.Invoke(kernel, invokeArgs);
        }
    }
}