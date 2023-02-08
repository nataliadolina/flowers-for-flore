using DI.Attributes.Register;
using Game.Characters.Runtimes.Interfaces;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using UnityEngine;

namespace Game.Characters.Runtimes
{
    [Register(typeof(IRuntime))]
    internal abstract class BaseRuntime : MonoBehaviour, IRuntime, IKernelEntity
    {
        private protected bool _isRunning = false;
        public abstract RuntimeType RuntimeType { get; }

        public virtual void OnStartRunning()
        {
            _isRunning = true;
        }

        public virtual void OnStopRunning()
        {
            _isRunning = false;
        }

        public abstract void Run();
    }
}
