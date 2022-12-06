using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using Game.Characters.Interfaces;
using System;
using DI.Kernel.Enums;
using Game.Characters.Enums;
using Game.Characters.Handlers;

namespace Game.Characters
{
    [Register]
    internal class Player : MonoBehaviour, IKernelEntity
    {
        private void OnChestEntityContactedPlayer(Transform chestEntityTransform, OwnerType ownerType)
        {
            if (ownerType == OwnerType.Flower)
            {
                _flowerContainer.SetFlowerParent(chestEntityTransform);
            }
        }

#region Kernel Entity

        [ConstructField(KernelTypeOwner.LogicScene)]
        private IPlayerContact[] _playerContacts;

        [ConstructField]
        private FlowersContainer _flowerContainer;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            SetSubstriptions();
        }

        private void SetSubstriptions()
        {
            foreach (var playerContact in _playerContacts)
            {
                playerContact.onChestEntityContancedPlayer += OnChestEntityContactedPlayer;
            }
        }

#endregion
    }
}
