using Game.Characters.Runtimes.Interfaces;
using Game.Characters.Enums;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using UnityEngine;

namespace Game.Characters.Runtimes
{
    [Register(typeof(IRuntime))]
    internal class EmptyRuntime : MonoBehaviour, IRuntime, IKernelEntity
    {
        public RuntimeType RuntimeType { get => RuntimeType.Empty; }
        public void Run()
        {

        }
    }
}
