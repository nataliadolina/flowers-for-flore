using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Kernel.Abstract;
using DI.Kernel.Enums;
using DI.Kernel.Interfaces;
using System.Linq;
using Utilities.Extensions;
using DI.Extensions;
using System;

namespace DI.Kernel
{
    /// <summary>
    /// Finds all kernel entities on scene and registeres them
    /// </summary>
    internal class SceneKernel : BaseMonoKernel
    {
        [SerializeField]
        private KernelTypeOwner kernelTypeOwner;

        private IKernelEntity[] _allInjections;

        public override KernelContextType KernelContextType { get => KernelContextType.Scene; }
        public override KernelTypeOwner KernelTypeOwner { get => kernelTypeOwner; }

        public override void RegisterInjections()
        {
            List<MonoBehaviour> objects = FindObjectsOfType<MonoBehaviour>().Where(x => x.Implements<IKernelEntity>()).ToList();
            int length = objects.Count();
            _allInjections = new IKernelEntity[length];
            _injectionsToConstruct = GetComponentsInChildren<IKernelEntity>();

            for (int i = 0; i < length; i++)
            {
                _allInjections[i] = objects[i].GetComponent<IKernelEntity>();
            }

            Array.ForEach(_allInjections, kernelEntityObject => kernelEntityObject.RegisterFromClassAttribute(this, kernelEntityObject.GetType()));
        }
    }
}  
