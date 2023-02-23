using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.States.Managers;
using Game.Characters.Runtimes;
using System;
using System.Linq;
using Utilities.Utils;
using System.Collections.Generic;

namespace Game.Characters.Handlers
{
    [Register]
    [Register(typeof(IStateManager))]
    internal class StateManager : MonoBehaviour, IKernelEntity, IStateManager
    {
        [SerializeField]
        private StateEntityType stateOnOutsideAllZones;

        [SerializeField]
        private List<ZoneProcessorStateArgs> zoneProcessorPriorityMap;

#region ISetActive

        private bool _isActive = true;

        public bool IsActive 
        { 
            get => _isActive;
            set => _isActive = value;
        }

#endregion

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }

            ZoneProcessorStateArgs[] maxPriorityArgs = zoneProcessorPriorityMap.Where(x => x.DistanceToSubjectZoneProcessor.IsSubjectInsideZone == true).OrderByDescending(x => x.Priority).ToArray();

            if (maxPriorityArgs.Length == 0)
            {
                _movingAgent.ChangeCurrentState(stateOnOutsideAllZones);
                return;
            }

            var maxPriorityArg = maxPriorityArgs[0];
            DistanceToSubjectZoneProcessor distanceToSubjectZoneProcessor = maxPriorityArg.DistanceToSubjectZoneProcessor;

            _movingAgent.ChangeCurrentState(maxPriorityArg.StateEntityTypeOnEnterZone);
        }

#region KernelEntity

        [ConstructField]
        private MovingAgent _movingAgent;

#endregion
    }
}
