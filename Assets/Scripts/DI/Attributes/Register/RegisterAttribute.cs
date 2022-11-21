using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Interfaces;

namespace DI.Attributes.Register
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
    internal class RegisterAttribute : Attribute
    {
        private readonly List<Type> _registerTypes = null;

        internal RegisterAttribute(params Type[] types)
        {
            _registerTypes = new List<Type>(types.Length);
            _registerTypes.AddRange(types);
        }

        internal void Register(IKernel kernel, object registerObject)
        {
            Debug.Log($"register types - {_registerTypes}");
            
            if (CheckRegisterTypes())
            {
                _registerTypes.ForEach(registerType => kernel.RegisterInjection(registerType, registerObject));
                Debug.Log($"Kernel type - {kernel.KernelTypeOwner}, Register types - {String.Join(", ", _registerTypes)}");
            }
            else
            {
                kernel.RegisterInjection(registerObject.GetType(), registerObject);
                Debug.Log($"Kernel type - {kernel.KernelTypeOwner}, Register object - {registerObject}");
            }
        }

        private bool CheckRegisterTypes()
        {
            if (_registerTypes == null)
            {
                return false;
            }
            if (_registerTypes.Count == 0)
            {
                return false;
            }
            return true;
        }
    }

}