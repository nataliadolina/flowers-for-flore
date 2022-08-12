using DI.Kernel.Interfaces;
using System;
using DI.Extensions;
using DI.Kernel.Abstract;

namespace DI.Kernel
{
    internal class LabirintKernel : BaseMonoKernel
    {
        private protected override void CreateInjections()
        {
            var injections = GetComponentsInChildren<IKernelEntity>(true);
            Array.ForEach(injections, kernelEntityObject => kernelEntityObject.CreateFromMethodAttribute(this, kernelEntityObject.GetType()));
        }
    }
}
