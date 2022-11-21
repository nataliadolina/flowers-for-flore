using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Interfaces;
using DI.Kernel.Abstract;
using DI.Kernel.Enums;

namespace DI.Kernel
{
    internal class KernelManager : MonoBehaviour
    {
        internal static KernelManager Instance;

        internal Dictionary<KernelTypeOwner, List<IKernel>> KernelOwnerMap = new Dictionary<KernelTypeOwner, List<IKernel>>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            IKernel[] kernels = FindObjectsOfType<BaseMonoKernel>();
            
            foreach (IKernel kernel in kernels)
            {   
                KernelTypeOwner owner = kernel.KernelTypeOwner;
                if (!KernelOwnerMap.ContainsKey(owner))
                {
                    KernelOwnerMap.Add(owner, new List<IKernel>());
                }

                KernelOwnerMap[owner].Add(kernel);
            }

            foreach (KernelTypeOwner TypeOwner in Enum.GetValues(typeof(KernelTypeOwner)))
            {
                if (KernelOwnerMap.TryGetValue(TypeOwner, out List<IKernel> kernelsToInitialize))
                {
                    kernelsToInitialize.ForEach(kernel => kernel.Initialize());
                }
            }
        }
    }
}
