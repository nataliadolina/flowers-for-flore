using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using System;
using DI.Extensions;
using System.Linq;

namespace DI.Kernel.Abstract
{
    internal abstract class BaseMonoKernel : MonoBehaviour, IKernel
    {
        private readonly IDictionary<Type, List<object>> _injectionsMap = new Dictionary<Type, List<object>>();

        private protected IKernelEntity[] _injectionsToConstruct;

        public abstract KernelContextType KernelContextType { get; }
        public abstract KernelTypeOwner KernelTypeOwner { get; }

        public virtual void RegisterInjections() { }

        public void ConstructInjections() {
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.ConstructFromFieldAttribute(this, kernelEntityObject.GetType()));
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.ConstructFromMethodAttribute(this, kernelEntityObject.GetType()));
        }

        public void RunInjections()
        {
            if (KernelTypeOwner == KernelTypeOwner.Creature)
            {
                Debug.Log("Monster on construct");
            }
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.RunFromMethodAttribute(this, kernelEntityObject.GetType()));
        }

        public void RegisterInjection(Type registerType, object kernelEntity)
        {
            if (!_injectionsMap.ContainsKey(registerType))
            {
                _injectionsMap[registerType] = new List<object>();
            }
            _injectionsMap[registerType].Add(kernelEntity);
        }

        public List<object> GetReflectionInjections(Type type)
        {
            if (!_injectionsMap.TryGetValue(type, out List<object> objectInstances) || objectInstances == null)
            {
                return new List<object>();
            }
            return objectInstances;
        }

        public object GetReflectionInjection(Type type)
        {
            IEnumerable<object> objectInstances = GetReflectionInjections(type);
            return objectInstances.Any()
                ? objectInstances.FirstOrDefault()
                : null;
        }

        public List<T> GetInjections<T>(Func<T, bool> predicate = null) where T : class
        {
            Type type = typeof(T);
            if (!_injectionsMap.TryGetValue(type, out List<object> objectInstances) || objectInstances == null)
            {
                return new List<T>();
            }
            return objectInstances.OfType<T>().ToList();
        }

        /// <summary>
        /// Возвращает первый объект, по которому выполняется условие предиката.
        /// Если предикат не передан, то возвращается первый попавшийся объект.
        /// </summary>
        public T GetInjection<T>(Func<T, bool> predicate = null) where T : class
        {
            var injections = GetInjections<T>();
            return predicate == null
                ? injections.FirstOrDefault()
                : injections.FirstOrDefault(predicate);
        }

#region Log

        private protected void ShowCollectedInjections()
        {
            foreach (var key in _injectionsMap.Keys)
            {
                if (_injectionsMap.TryGetValue(key, out List<object> value))
                {
                    Debug.Log($"{ key } : {value}, {value.Count}");
                    value.ForEach(x => Debug.Log(x));
                }
            }
        }

#endregion
    }
}
