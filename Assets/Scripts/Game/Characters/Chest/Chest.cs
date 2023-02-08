using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Run;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.Effects;
using DI.Kernel.Enums;
using System;
using Game.Characters.Utilities.Utils;
using Game.Characters.States.Managers;

namespace Game.Characters.Chest
{
    [Register(typeof(IChest))]
    internal class Chest : MonoBehaviour, IKernelEntity, IChest
    {
        public event Action onOpened;
        public event Action<DistanceToPlayerArgs> onPlayerEnteredChestZone;
        public event Action<IChest> onPlayerExitedChestZone;

        private bool _canBeOpened;

#region ITransform

        public Transform Transform { get; private set; }

#endregion

#region IChest

        public bool CanBeOpened { get => _canBeOpened;}

        public void Open()
        {
            _chestEntityTransform.parent = null;
            onOpened?.Invoke();
        }

        public void Destroy()
        {
            ClearSubscriptions();
            Destroy(gameObject);
        }

#endregion

        private void OnPlayerEnteredChestZone(Transform playerTransform)
        {
            _canBeOpened = true;
            onPlayerEnteredChestZone?.Invoke(new DistanceToPlayerArgs(Transform, playerTransform, this));
        }

        private void OnPlayerExitedChestZone()
        {
            _canBeOpened = false;
            onPlayerExitedChestZone?.Invoke(this);
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Kernel Entity

        private Transform _chestEntityTransform;
        private IDistanceToSubjectZoneProcessor _distanceToPlayerProcessor;

        [ConstructField]
        private IChestAnimator _chestAnimator;

        [ConstructField]
        private MovingAgent _movingAgent;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            Transform = transform;
            _distanceToPlayerProcessor = kernel.GetInjection<IDistanceToSubjectZoneProcessor>(x => x.OwnerType == OwnerType.Chest && x.AimType == OwnerType.Player);
            SetSubscriptions();
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _chestEntityTransform = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity).Transform;
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _distanceToPlayerProcessor.onAimEnterZone += OnPlayerEnteredChestZone;
            _distanceToPlayerProcessor.onAimExitZone += OnPlayerExitedChestZone;
       
            if (_movingAgent != null)
            {
                _chestAnimator.onOpenAnimationStoppedPlaying += _movingAgent.TerminateCurrentState;
            }
        }

        private void ClearSubscriptions()
        {
            _distanceToPlayerProcessor.onAimEnterZone -= OnPlayerEnteredChestZone;
            _distanceToPlayerProcessor.onAimExitZone -= OnPlayerExitedChestZone;

            if (_movingAgent != null)
            {
                _chestAnimator.onOpenAnimationStoppedPlaying -= _movingAgent.TerminateCurrentState;
            }
        }

#endregion

    }
}
