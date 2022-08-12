using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Kernel.Interfaces;
using System;
using DI.Extensions;
using System.Linq;

namespace DI.Kernel.Abstract
{
    internal abstract class BaseMonoKernel : MonoBehaviour, IKernel
    {
        private IDictionary<Type, List<object>> _injectionsMap = new Dictionary<Type, List<object>>();
        private IKernelEntity[] _injectionsToConstruct;

        public void Initialize()
        {
            CreateInjections();
            RegisterInjections();
            ConstructInjections();
            RunInjections();
        }

        private protected virtual void CreateInjections() { }

        private void RegisterInjections()
        {
            _injectionsToConstruct = GetComponentsInChildren<IKernelEntity>(true);
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.RegisterFromClassAttribute(this, kernelEntityObject.GetType()));
        }

        private void ConstructInjections()
        {
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.ConstructFromFieldAttribute(this, kernelEntityObject.GetType()));
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.ConstructFromMethodAttribute(this, kernelEntityObject.GetType()));
        }

        private void RunInjections()
        {
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

        public object ConstructInjection(Type type)
        {
            if (_injectionsMap.TryGetValue(type, out List<object> listOfEntities))
            {
                return listOfEntities[0];
            }

            return null;
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
