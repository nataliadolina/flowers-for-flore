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
        [SerializeField] private float score = 0;
        [SerializeField] private TriggerEnterHandler triggerEnterHandler;

        private void OnChestEntityContactedPlayer(Transform chestEntityTransform, OwnerType ownerType)
        {
            if (ownerType == OwnerType.Flower)
            {
                _flowerContainer.SetFlowerParent(chestEntityTransform);
            }
        }


#region Kernel Entity

        // toDo: сделать возможным извлекание инъекций из других ядер (ошибка KeyNotFound: ConstructField выполняется раньше чем Register в ядре ChestEntity)
        // решение - сначала вызывать Register во всех ядрах, а затем Construct во всех ядрах?
        [ConstructField(KernelTypeOwner.ChestEntity)]
        private IPlayerContact _playerContact;

        [ConstructField]
        private FlowerContainer _flowerContainer;

        private void SetSubstriptions()
        {
            _playerContact.onChestEntityContancedPlayer += OnChestEntityContactedPlayer;
        }

#endregion
    }
}
