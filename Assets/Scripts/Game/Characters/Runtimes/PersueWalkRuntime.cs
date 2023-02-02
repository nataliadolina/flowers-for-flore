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
            _movingAgent.CurrentState.Run();
        }

        private void ToPersueState(float distance)
        {
            if (!_isRunning)
            {
                return;
            }
            _movingAgent.CurrentState = _persueState;
        }

        private void ToWalkState()
        {
            if (!_isRunning)
            {
                return;
            }
            _movingAgent.CurrentState = _walkState;
        }

#region Kernel entity

        [ConstructField]
        private MovingAgent _movingAgent;

        private IStateEntity _persueState;

        private IStateEntity _walkState;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _persueState = kernel.GetInjection<IStateEntity>(x => x.StateEntityType == StateEntityType.Persue);
            _walkState = kernel.GetInjection<IStateEntity>(x => x.StateEntityType == StateEntityType.Walk);

            IDistanceToSubjectZoneProcessor distanceToSubjectZoneProcessor = kernel.GetInjection<IDistanceToSubjectZoneProcessor>(x => x.OwnerType == OwnerType.Runtime && x.AimType == OwnerType.Player);
            distanceToSubjectZoneProcessor.onAimEnterZone += ToPersueState;
            distanceToSubjectZoneProcessor.onAimExitZone += ToWalkState;
        }

#endregion
    }
}
