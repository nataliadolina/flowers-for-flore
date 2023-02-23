using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using Game.Characters.States;
using Game.Characters.Enums;
using Game.Characters.Handlers;
using Game.Characters.States.Managers;


namespace Game.Characters.Effects
{
    [Register]
    internal class RadarLightManager : MonoBehaviour, IKernelEntity
    {
        [SerializeField]
        private Light light;

        private void SetLightEnabled(StateEntityType stateEntityType)
        {
            bool enabled = stateEntityType == StateEntityType.Still ? false : stateEntityType == StateEntityType.Persue || stateEntityType == StateEntityType.Attack ? true : false;
            SetLightEnabled(enabled);
        }

        private void SetLightEnabled(bool enabled)
        {
            light.enabled = enabled;
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Kernel Entity

        [ConstructField]
        private MovingAgent _movingAgent;

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            SetLightEnabled(false);
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _movingAgent.onCurrentStateChanged += SetLightEnabled;
        }

        private void ClearSubscriptions()
        {
            _movingAgent.onCurrentStateChanged -= SetLightEnabled;
        }

#endregion
    }
}