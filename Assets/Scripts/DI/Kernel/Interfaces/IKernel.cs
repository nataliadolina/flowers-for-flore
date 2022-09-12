using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Enums;

namespace DI.Kernel.Interfaces
{
    internal interface IKernel
    {
        KernelType KernelType { get; }
        void Initialize();
        void RegisterInjection(Type type, object kernelEntity);
        object ConstructInjection(Type type);
        List<object> GetReflectionInjections(Type type);
        object GetReflectionInjection(Type type);
        T GetInjection<T>(Func<T, bool> predicate = null) where T : class;
        List<T> GetInjections<T>(Func<T, bool> predicate = null) where T : class;
    }
}