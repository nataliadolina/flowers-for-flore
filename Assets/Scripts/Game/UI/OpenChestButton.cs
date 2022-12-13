using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI.Abstract;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using Game.Characters.Interfaces;

namespace Game.UI
{
    [Register]
    internal class OpenChestButton : ClickableBase, IKernelEntity
    {
        private protected override void OnClick()
        {
            _chestsContainer.OpenSelectedChest();
        }

        private void SetActive(bool active)
        {
            _buttonGameObject.SetActive(active);
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubstriptions();
        }

#endregion

#region Kernel Entity

        [ConstructField(KernelTypeOwner.LogicScene)]
        private IChestsContainer _chestsContainer;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            SetActive(false);
            SetSubstriptions();
        }

#endregion

#region Substriptions

        private void SetSubstriptions()
        {
            _chestsContainer.onIsThereAnyOpenableChestsChanged += SetActive;
        }

        private void ClearSubstriptions()
        {
            _chestsContainer.onIsThereAnyOpenableChestsChanged -= SetActive;
        }

#endregion
    }
}