using UnityEngine;
using Game.Characters.States.Managers;
using DI.Kernel.Interfaces;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using Game.Characters.Interfaces;
using Game.Characters.Enums;

namespace Game.Characters.States.Abstract
{
    [Register(typeof(IStateEntity))]
    internal abstract class BaseState : MonoBehaviour, IKernelEntity, IStateEntity
    {   
        [SerializeField] private StateEntityType nextState;

        public abstract StateEntityType StateEntityType { get; }

        public abstract void Run();

        public void Terminate()
        {
            BeforeTerminate();
            _movingAgent.ChangeCurrentState(nextState);
        }

        public virtual void BeforeTerminate() { }
        public virtual void OnStartState() { }

#region Kernel Entity

        [ConstructField]
        private protected MovingAgent _movingAgent;

#endregion

    }
}
