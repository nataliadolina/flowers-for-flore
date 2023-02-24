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

        private Queue<IKernel> _kernelsToProccess = new Queue<IKernel>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ProcessAllKernelsOnScene();
        }

        internal void AddKernel(IKernel kernel)
        {
            RegisterKernel(kernel);
            ProcessKernel(kernel);
        }

        private void ProcessAllKernelsOnScene()
        {
            IKernel[] kernels = FindObjectsOfType<BaseMonoKernel>();

            foreach (IKernel kernel in kernels)
            {
                KernelTypeOwner owner = kernel.KernelTypeOwner;
                RegisterKernel(kernel);
            }

            ProcessAllKernels(kernels);
        }

        private void RegisterKernel(IKernel kernel)
        {
            KernelTypeOwner kernelOwner = kernel.KernelTypeOwner;
            if (!KernelOwnerMap.ContainsKey(kernelOwner))
            {
                KernelOwnerMap.Add(kernelOwner, new List<IKernel>());
            }

            KernelOwnerMap[kernelOwner].Add(kernel);
        }

        private void ProcessAllKernels(IKernel[] kernels)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (IKernel kernel in kernels)
                {
                    KERNEL_METHODS_ORDER[i].Invoke(kernel, new object[0]);
                }
            }
        }

        private void ProcessKernel(IKernel kernel)
        {
            for (int i = 0; i < 3; i++)
            {
                KERNEL_METHODS_ORDER[i].Invoke(kernel, new object[0]);
            }
        }
    }
}
