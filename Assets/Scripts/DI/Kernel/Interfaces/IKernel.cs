using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Enums;

namespace DI.Kernel.Interfaces
{
    internal interface IKernel
    {
        KernelContextType KernelContextType { get; }

        KernelTypeOwner KernelTypeOwner { get; }

        /// <summary>
        /// Registers all injections in kernel
        /// </summary>
        void RegisterInjections();

        /// <summary>
        /// Constructs all injections in kernel
        /// </summary>
        void ConstructInjections();

        /// <summary>
        /// Runs all injections in kernel
        /// </summary>
        void RunInjections();

        /// <summary>
        /// Registers kernel entity using Reflection. Called from Register attribute.
        /// </summary>
        /// <param name="type">Used as key for injection.</param>
        /// <param name="kernelEntity">Injection value.</param>
        void RegisterInjection(Type type, object kernelEntity);

        /// <summary>
        /// Gets the list of injections from kernel using reflection. Called in construct attribute.
        /// </summary>
        /// <param name="type">Type of injections we want to get</param>
        /// <returns>The generic list of kernel entities, which later will be casted to the needed type.</returns>
        List<object> GetReflectionInjections(Type type);

        /// <summary>
        /// Gets the injection from kernel using reflection. Called in construct attribute.
        /// </summary>
        /// <param name="type">Type of injection we want to get.</param>
        /// <returns>Kernel entity, which later will be casted to the needed type.</returns>
        object GetReflectionInjection(Type type);

        /// <summary>
        /// Allows to manualy get the injection of specific type. Can be called in kernel entity Construct/Run methods.
        /// </summary>
        /// <param name="type">Type of injection we want to get.</param>
        /// <param name="predicate">Condition we want to apply to filter injections. Null by default</param>
        /// <returns>The injection.</returns>
        T GetInjection<T>(Func<T, bool> predicate = null) where T : class;

        /// <summary>
        /// Allows to manualy get the list of injections of specific type. Can be called in kernel entity Construct/Run methods.
        /// </summary>
        /// <typeparam name="T">Type of injections</typeparam>
        /// <param name="predicate">Condition we want to apply to filter injections. Null by default</param>
        /// <returns>The list of injections.</returns>
        List<T> GetInjections<T>(Func<T, bool> predicate = null) where T : class;
    }
}