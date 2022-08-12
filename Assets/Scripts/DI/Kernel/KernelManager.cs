using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DI.Kernel.Interfaces;

namespace DI.Kernel
{
    internal class KernelManager : MonoBehaviour
    {
        private IKernel[] kernels;

        private void Start()
        {
            Array.ForEach(kernels, kernel => kernel.Initialize());
        }
    }
}
