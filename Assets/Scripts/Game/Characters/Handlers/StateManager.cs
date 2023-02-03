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
        [SerializeField]
        private DistanceToSubjectZoneProcessor distanceToPlayerProcessor;

        private void OnSubjectEnter(float distance)
        {
            _movingAgent.TerminateCurrentState();
        }

        private void OnSubjectLeave()
        {
            _movingAgent.TerminateCurrentState();
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

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            distanceToPlayerProcessor.onAimEnterZone += OnSubjectEnter;
            distanceToPlayerProcessor.onAimExitZone += OnSubjectLeave;
        }

        private void ClearSubscriptions()
        {
            distanceToPlayerProcessor.onAimEnterZone -= OnSubjectEnter;
            distanceToPlayerProcessor.onAimExitZone -= OnSubjectLeave;
        }

#endregion
    }
}
