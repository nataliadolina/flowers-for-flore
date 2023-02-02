using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.States.Managers;

namespace Game.Characters.Handlers
{
    [Register]
    internal class StateManager : MonoBehaviour, IKernelEntity
    {
        private void OnSubjectEnter(float distance)
        {
            _movingAgent.CurrentState.Terminate();
        }

        private void OnSubjectLeave()
        {
            _movingAgent.CurrentState.Terminate();
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region KernelEntity

        [ConstructField]
        private MovingAgent _movingAgent;

        private IDistanceToSubjectZoneProcessor _distanceToPlayerProcessor;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _distanceToPlayerProcessor = kernel.GetInjection<IDistanceToSubjectZoneProcessor>(x => x.OwnerType == OwnerType.ChestEntity && x.AimType == OwnerType.Player);
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _distanceToPlayerProcessor.onAimEnterZone += OnSubjectEnter;
            _distanceToPlayerProcessor.onAimExitZone += OnSubjectLeave;
        }

        private void ClearSubscriptions()
        {
            _distanceToPlayerProcessor.onAimEnterZone -= OnSubjectEnter;
            _distanceToPlayerProcessor.onAimExitZone -= OnSubjectLeave;
        }

#endregion
    }
}
