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

namespace Game.Characters.Chest
{
    [Register(typeof(IChest))]
    internal class Chest : MonoBehaviour, IKernelEntity, IChest
    {
        public event Action onOpened;
        public event Action<DistanceToPlayerArgs> onPlayerEnteredChestZone;

        [SerializeField] private Particles appearParticles;
        [SerializeField] private Particles destroyParticles;

        [SerializeField] private GameObject selectionAura;

        private bool _isSelected = false;
        private bool _isVisible = false;

        public bool IsSelected {
            get => _isSelected;
            set { 
                selectionAura.SetActive(value);
                _isSelected = value;
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                appearParticles.IsActivated = value;
            }
        }

        private void OnPlayerEnteredChestZone(float distanceToPlayer)
        {
            onPlayerEnteredChestZone?.Invoke(new DistanceToPlayerArgs(distanceToPlayer, this, true));
        }

#region ITransform

        public Transform Transform { get; private set; }

#endregion

#region IChest

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

#region Kernel Entity

        private Transform _chestEntityTransform;
        private IDistanceToSubjectZoneProcessor _distanceToPlayerProcessor;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            Transform = transform;
            _distanceToPlayerProcessor = kernel.GetInjection<IDistanceToSubjectZoneProcessor>(x => (x.OwnerType == OwnerType.ChestEntity | x.OwnerType == OwnerType.Chest) && x.AimType == OwnerType.Player);
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
        }

        private void ClearSubscriptions()
        {
            _distanceToPlayerProcessor.onAimEnterZone -= OnPlayerEnteredChestZone;
        }

#endregion

    }
}
