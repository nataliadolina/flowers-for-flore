using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;
using Game.Characters.Abstract;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;

namespace Game.Characters.Monster
{
    internal class MonsterInsideChestHandler : BaseChestEntity
    {
        [SerializeField]
        private GameObject currentMesh;

        private void OnMonsterAppear()
        {
            IsActive = true;
        }

#region Kernel Entity

        [ConstructField]
        private IAppearState appearState;

        [ConstructField]
        private IChestAnimator _chestAnimator;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _currentMesh = currentMesh;
            IsActive = false;
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _chestAnimator.onOpenAnimationStoppedPlaying += OnMonsterAppear;
        }

#endregion
    }
}
