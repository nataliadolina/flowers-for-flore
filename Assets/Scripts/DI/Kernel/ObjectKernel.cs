using DI.Kernel.Abstract;
using System;
using DI.Extensions;
using UnityEngine;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;

namespace DI.Kernel
{
    internal class ObjectKernel : BaseMonoKernel {
        [SerializeField]
        private KernelTypeOwner kernelTypeOwner;

        public override KernelContextType KernelContextType { get => KernelContextType.Object; }
        public override KernelTypeOwner KernelTypeOwner { get => kernelTypeOwner; }

        public override void RegisterInjections()
        {
            _injectionsToConstruct = GetComponentsInChildren<IKernelEntity>(true);
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.RegisterFromClassAttribute(this, kernelEntityObject.GetType()));
        }

        public override void ConstructInjections()
        {
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.ConstructFromFieldAttribute(this, kernelEntityObject.GetType()));
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.ConstructFromMethodAttribute(this, kernelEntityObject.GetType()));
        }

        public override void RunInjections()
        {
            Array.ForEach(_injectionsToConstruct, kernelEntityObject => kernelEntityObject.RunFromMethodAttribute(this, kernelEntityObject.GetType()));
        }
    }
}
