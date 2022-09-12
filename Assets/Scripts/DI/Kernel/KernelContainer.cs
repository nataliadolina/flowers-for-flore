using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Kernel.Enums;
using DI.Kernel.Interfaces;
using DI.Kernel.Abstract;

namespace DI.Kernel
{
    internal class KernelContainer : MonoBehaviour
    {
        private Dictionary<KernelType, List<IKernel>> kernelsMap = new Dictionary<KernelType, List<IKernel>>();

        internal static KernelContainer Instance;
        private void Awake()
        {
            GatherKernels();

            if (Instance == null || Instance != this)
            {
                Instance = this;
            }
        }

        private void GatherKernels()
        {
            var kernels = (object[])FindObjectsOfType<BaseMonoKernel>();
            foreach (var kernel in kernels)
            {
                var kernelType = (IKernel)kernel;
                var kernelObjectType = kernelType.KernelType;
                if (!kernelsMap.ContainsKey(kernelObjectType))
                {
                    kernelsMap.Add(kernelObjectType, new List<IKernel>());
                }
                
                kernelsMap[kernelObjectType].Add(kernelType);
                
            }
        }

        internal IKernel GetKernelByKey(KernelType kernelType)
        {
            return kernelsMap[kernelType][0];
        }

        internal List<IKernel> GetKernelsByKey(KernelType kernelType)
        {
            return kernelsMap[kernelType];
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}