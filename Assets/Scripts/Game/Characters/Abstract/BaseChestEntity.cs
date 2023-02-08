using UnityEngine;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;

namespace Game.Characters.Abstract
{
    [Register(typeof(IChestEntity))]
    internal abstract class BaseChestEntity : MonoBehaviour, IChestEntity, IKernelEntity
    {
        [SerializeField] private protected OwnerType ownerType;
        private protected GameObject _currentMesh;

#region IOwnerType

        public OwnerType OwnerType { get => ownerType; }

#endregion

#region ISetActive

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                _currentMesh.SetActive(value);
            }
        }

#endregion
    }
}
