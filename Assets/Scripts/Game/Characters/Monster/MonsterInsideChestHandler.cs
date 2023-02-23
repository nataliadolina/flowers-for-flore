using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;
using Game.Characters.Abstract;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using Game.Characters.States.Managers;
using Game.Characters.Enums;

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

        private void OnAppearStateTerminated()
        {
            _stateManager.IsActive = true;
        }

#region Kernel Entity

        [ConstructField]
        private IChestAnimator _chestAnimator;

        [ConstructField]
        private IStateManager _stateManager;

        [ConstructField]
        private IAppearState _appearState;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _currentMesh = currentMesh;
            _stateManager.IsActive = false;
            IsActive = false;
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _chestAnimator.onOpenAnimationStoppedPlaying += OnMonsterAppear;
            _appearState.onAppearStateTerminated += OnAppearStateTerminated;
        }

#endregion
    }
}
