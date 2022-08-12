using DI.Kernel.Interfaces;
using System;
using DI.Attributes.Create;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Utilities.Extensions;
using Utilities.Utils;

namespace DI.Extensions
{
    internal static class KernelExtensions
    {
        private const BindingFlags BINDING_FLAG = BindingFlags.NonPublic | BindingFlags.Instance;
        /// <summary>
        /// Функция вызова регистрации сущности в ядре 
        /// </summary>
        internal static void RegisterFromClassAttribute(this IKernelEntity kernelEntity, IKernel kernel, Type kernelEntityType)
        {
            foreach (var attribute in kernelEntityType.GetCustomAttributes<RegisterAttribute>())
            {
                attribute.Register(kernel, kernelEntity);
            }
        }

        /// <summary>
        /// Функция вызова поиска сущностей в ядре 
        /// </summary>
        internal static void ConstructFromFieldAttribute(this IKernelEntity kernelEntity, IKernel kernel, Type kernelEntityType)
        {
            FieldInfo[] fieldInfos = kernelEntityType.GetFields(BINDING_FLAG);
            foreach (var fieldInfo in fieldInfos)
            {
                var attribute = fieldInfo.GetCustomAttribute<ConstructFieldAttribute>();
                if (attribute != null)
                {
                    Type fieldType = fieldInfo.FieldType;
                    if (fieldType.IsMultiple())
                    {
                        Type elementType = fieldInfo.FieldType.GetElementType();
                        IEnumerable<object> value = attribute.FindInstances(kernel, elementType);
                        IEnumerable<object> existedValue = (IEnumerable<object>)fieldInfo.GetValue(kernelEntity);
                        if (fieldType.IsArray)
                        {
                            if (existedValue == null)
                            {
                                fieldInfo.SetValue(kernelEntity, value.ToArray(elementType));
                            }
                            else
                            {
                                fieldInfo.SetValue(kernelEntity, GenericCollectionExtensions.JoinToArray((Array)existedValue, elementType, value));
                            }
                        }
                        else
                        {
                            fieldInfo.SetValue(kernelEntity, ((IList)existedValue).AddRange(value));
                        }
                    }
                    else
                    {
                        fieldInfo.SetValue(kernelEntity, attribute.FindInstance(kernel, fieldType));
                    }
                }
            }
        }

        internal static void CreateFromMethodAttribute(this IKernelEntity kernelEntity, IKernel kernel, Type kernelEntityType)
        {
            MethodInfo[] methodInfos = kernelEntityType.GetMethods(BINDING_FLAG);
            foreach (var methodInfo in methodInfos)
            {
                var attribute = methodInfo.GetCustomAttribute<CreateMethodAttribute>();
                if (attribute != null)
                {
                    object[] parameters = { kernel };
                    methodInfo.Invoke(kernelEntity, parameters);
                }
            }
        }

        internal static void ConstructFromMethodAttribute(this IKernelEntity kernelEntity, IKernel kernel, Type kernelEntityType)
        {
            MethodInfo[] methodInfos = kernelEntityType.GetMethods(BINDING_FLAG);
            foreach (var methodInfo in methodInfos)
            {
                var attribute = methodInfo.GetCustomAttribute<ConstructMethodAttribute>();
                if (attribute != null)
                {
                    object[] parameters = { kernel };
                    methodInfo.Invoke(kernelEntity, parameters);
                }
            }
        }

        internal static void RunFromMethodAttribute(this IKernelEntity kernelEntity, IKernel kernel, Type kernelEntityType)
        {
            MethodInfo[] methodInfos = kernelEntityType.GetMethods(BINDING_FLAG);
            foreach (var methodInfo in methodInfos)
            {
                var attribute = methodInfo.GetCustomAttribute<RunMethodAttribute>();
                if (attribute!= null)
                {
                    object[] parametres = { kernel };
                    methodInfo.Invoke(kernelEntity, parametres);
                }
            }
        }
    }
}
