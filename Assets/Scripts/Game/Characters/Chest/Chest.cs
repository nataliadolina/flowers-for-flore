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
            Destroy(gameObject);
        }

#endregion

#region Kernel Entity

        private Transform _chestEntityTransform;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            Transform = transform;
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            _chestEntityTransform = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity).Transform;
        }

#endregion

    }
}
