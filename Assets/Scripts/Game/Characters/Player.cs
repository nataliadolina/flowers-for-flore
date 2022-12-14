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
        private void OnChestEntityContactedPlayer(IBody chestEntityBody, OwnerType ownerType, StateEntityType currentStateType)
        {
            if (ownerType == OwnerType.Flower && currentStateType == StateEntityType.Persue)
            {
                chestEntityBody.SetRigidbodiesEnabled(false);
                chestEntityBody.SetCollisionDetectorsEnabled(false);
                _flowerContainer.SetFlowerParent(chestEntityBody.Transform);
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
