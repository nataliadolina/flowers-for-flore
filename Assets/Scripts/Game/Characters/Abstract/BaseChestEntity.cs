using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;

namespace Game.Characters.Abstract
{
    [Register(typeof(IChestEntity))]
    [Register(typeof(IOwnerType))]
    internal abstract class BaseChestEntity : ChestEntityPhysics, IChestEntity
    {
        [SerializeField] private protected OwnerType ownerType;
        private protected GameObject _currentMesh = null;

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
                if (_isActive != value)
                {
                    _isActive = value;
                    _currentMesh.SetActive(value);
                }
            }
        }

#endregion
    }
}
