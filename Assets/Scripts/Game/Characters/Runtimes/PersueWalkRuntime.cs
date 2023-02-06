using UnityEngine;
using Game.Characters.States.Managers;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using Game.Characters.Runtimes.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.Handlers;

namespace Game.Characters.Runtimes
{
    internal class PersueWalkRuntime : BaseRuntime
    {
        public override RuntimeType RuntimeType { get => RuntimeType.PersueWalk; }

        public override void Run()
        {
            //if (!_isRunning)
            //{
            //    return;
            //}

            //if (_distanceToSubjectZoneProcessor.IsSubjectInsideZone && _movingAgent.CurrentState.StateEntityType != StateEntityType.Persue)
            //{
            //    ToPersueState();
            //}
            //else if (!_distanceToSubjectZoneProcessor.IsSubjectInsideZone && _movingAgent.CurrentState.StateEntityType != StateEntityType.Walk)
            //{
            //    ToWalkState();
            //}
            _movingAgent.CurrentState.Run();
        }

        private void ToPersueState(float distance)
        {
            if (!_isRunning | _movingAgent.CurrentState.StateEntityType == StateEntityType.Attack)
            {
                return;
            }
            _movingAgent.CurrentState = _persueState;
        }

        private void ToWalkState()
        {
            if (!_isRunning | _movingAgent.CurrentState.StateEntityType == StateEntityType.Attack)
            {
                return;
            }
            _movingAgent.CurrentState = _walkState;
        }

#region Mono Behaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Kernel entity

        [ConstructField]
        private MovingAgent _movingAgent;

        private IStateEntity _persueState;

        private IStateEntity _walkState;

        private IDistanceToSubjectZoneProcessor _distanceToSubjectZoneProcessor;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _persueState = kernel.GetInjection<IStateEntity>(x => x.StateEntityType == StateEntityType.Persue);
            _walkState = kernel.GetInjection<IStateEntity>(x => x.StateEntityType == StateEntityType.Walk);

            _distanceToSubjectZoneProcessor = kernel.GetInjection<IDistanceToSubjectZoneProcessor>(x => x.OwnerType == OwnerType.Runtime && x.AimType == OwnerType.Player);
            SetSubscriprions();
        }

#endregion

#region Subscriptions

        private void SetSubscriprions()
        {
            _distanceToSubjectZoneProcessor.onAimEnterZone += ToPersueState;
            _distanceToSubjectZoneProcessor.onAimExitZone += ToWalkState;
        }

        private void ClearSubscriptions()
        {
            _distanceToSubjectZoneProcessor.onAimEnterZone -= ToPersueState;
            _distanceToSubjectZoneProcessor.onAimExitZone -= ToWalkState;
        }

#endregion
    }
}
