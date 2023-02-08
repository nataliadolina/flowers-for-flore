using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.States.Managers;
using Game.Characters.Runtimes;

namespace Game.Characters.Handlers
{
    [Register]
    internal class StateManager : MonoBehaviour, IKernelEntity
    {
        [SerializeField]
        private StateEntityType stateOnEnterZone;

        [SerializeField]
        private StateEntityType stateOnExitZone;

        [SerializeField]
        private DistanceToSubjectZoneProcessor distanceToPlayerProcessor;

        private void OnSubjectEnter(Transform aimTransform)
        {
            _movingAgent.ChangeCurrentState(stateOnEnterZone);
        }
       
        private void OnSubjectLeave()
        {
            _movingAgent.ChangeCurrentState(stateOnExitZone);
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

        [ConstructField]
        private PersueWalkRuntime _persueWalkRuntime;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            Debug.Log("Set subscriptions");
            _persueWalkRuntime.onPersueWalkRuntimeStartedRunning += SetDistanceProcessorSubscriptions;
            _persueWalkRuntime.onPersueWalkRuntimeStoppedRunning += ClearDistanceProcessorSubscriptions;
        }

        private void ClearSubscriptions()
        {
            _persueWalkRuntime.onPersueWalkRuntimeStartedRunning -= SetDistanceProcessorSubscriptions;
            _persueWalkRuntime.onPersueWalkRuntimeStoppedRunning -= ClearDistanceProcessorSubscriptions;
        }

        private void SetDistanceProcessorSubscriptions()
        {
            distanceToPlayerProcessor.onAimEnterZone += OnSubjectEnter;
            distanceToPlayerProcessor.onAimExitZone += OnSubjectLeave;
        }

        private void ClearDistanceProcessorSubscriptions()
        {
            distanceToPlayerProcessor.onAimEnterZone -= OnSubjectEnter;
            distanceToPlayerProcessor.onAimExitZone -= OnSubjectLeave;
        }

#endregion
    }
}
