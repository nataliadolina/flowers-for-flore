using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using DI.Kernel.Interfaces;
using DI.Kernel.Abstract;
using DI.Kernel.Enums;

namespace DI.Kernel
{
    internal class KernelManager : MonoBehaviour
    {
        internal static KernelManager Instance;

        internal Dictionary<KernelTypeOwner, List<IKernel>> KernelOwnerMap = new Dictionary<KernelTypeOwner, List<IKernel>>();
        private const string REGISTER_METHOD_NAME = nameof(IKernel.RegisterInjections);
        private const string CONSTRUCT_METHOD_NAME = nameof(IKernel.ConstructInjections);
        private const string RUN_METHOD_NAME = nameof(IKernel.RunInjections);

        private readonly MethodInfo[] KERNEL_METHODS_ORDER = new MethodInfo[3] { 
            typeof(IKernel).GetMethod(REGISTER_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance),
            typeof(IKernel).GetMethod(CONSTRUCT_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance),
            typeof(IKernel).GetMethod(RUN_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance), 
        };

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

            for (int i = 0; i < 3; i++)
            {
                foreach ( IKernel kernel in kernels)
                {
                    KERNEL_METHODS_ORDER[i].Invoke(kernel, new object[0]);
                }
            }
        }
    }
}
