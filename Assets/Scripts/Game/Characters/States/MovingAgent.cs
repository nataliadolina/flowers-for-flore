using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.States.Abstract;
using Game.Characters.Enums;
using Game.Characters.Runtimes.Interfaces;
using DI.Attributes.Run;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Handlers;

namespace Game.Characters.States.Managers
{
    [Register]
    internal class MovingAgent : MonoBehaviour, IKernelEntity
    {
        [SerializeField] private StateEntityType startState;
        [SerializeField] private RuntimeType startRuntime;

        private IStateEntity _currentState;
        private IRuntime _currentRuntime;

        public IStateEntity CurrentState { get { return _currentState; } set { _currentState = value; } }

        private void Update()
        {
            _currentRuntime.Run();
        }

        private void OnDistanceToPlayerChanged(float distanceToPlayer)
        {
            TerminateCurrentState();
        }

        internal void ChangeCurrentRuntime(RuntimeType runtimeType)
        {
            _currentRuntime = _runtimeEntitiesMap[runtimeType];
        }

        internal void ChangeCurrentState(StateEntityType toState)
        {
            _currentState = _stateEntitiesMap[toState];
            _currentState.OnStartState();
        }

        internal void TerminateCurrentState()
        {
            _currentState.Terminate();
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubstribtions();
        }

#endregion

#region KernelEntity

        [ConstructField]
        private IStateEntity[] _stateEntities;

        [ConstructField]
        private IRuntime[] _runtimes;

        private Dictionary<StateEntityType, IStateEntity> _stateEntitiesMap = new Dictionary<StateEntityType, IStateEntity>();

        private Dictionary<RuntimeType, IRuntime> _runtimeEntitiesMap = new Dictionary<RuntimeType, IRuntime>();

        [ConstructField]
        private DistanceToPlayerHandler _distanceToPlayerHandler;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            foreach (var entity in _stateEntities)
            {
                _stateEntitiesMap.Add(entity.StateEntityType, entity);
            }

            foreach (var runtime in _runtimes)
            {
                _runtimeEntitiesMap.Add(runtime.RuntimeType, runtime);
            }
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            SetSubscribtions();

            CurrentState = _stateEntitiesMap[startState];
            _currentRuntime = _runtimeEntitiesMap[startRuntime];
            _currentState.OnStartState();
        }

#endregion

#region Subscriptions

        private void SetSubscribtions()
        {
            _distanceToPlayerHandler.onDistanceToPlayerChange += OnDistanceToPlayerChanged;
        }

        private void ClearSubstribtions()
        {
            _distanceToPlayerHandler.onDistanceToPlayerChange -= OnDistanceToPlayerChanged;
        }

#endregion
    }
}