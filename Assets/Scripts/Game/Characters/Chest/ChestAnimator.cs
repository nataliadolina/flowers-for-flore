using System.Collections;
using System;
using UnityEngine;
using Game.Characters.Abstract;
using DI.Kernel.Interfaces;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using Game.Characters.Interfaces;

namespace Game.Characters.Chest
{
    [Register(typeof(IChestAnimator))]
    internal class ChestAnimator : AnimatorHandlerBase, IKernelEntity, IChestAnimator
    {
        public event Action onOpenAnimationStoppedPlaying;

        private readonly int openTriggerAnimationIndex = Animator.StringToHash("open");
        private readonly int openAnimationIndex = Animator.StringToHash("Open");
        
        private void PlayOpenAnimation()
        {
            PlayAnimationByTriggerThenDestroyGameObject(gameObject, openAnimationIndex, openTriggerAnimationIndex);
        }

        private protected override void AnimationStoppedPlaying(int animationIndex)
        {
            if (animationIndex == openAnimationIndex)
            {
                onOpenAnimationStoppedPlaying?.Invoke();
            }
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubstriptions();
        }

#endregion

#region Kernel Entity

        [ConstructField]
        private IChest _chest;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            SetSubstriptions();
        }

#endregion

#region Substriptions

        private void SetSubstriptions()
        {
            _chest.onOpened += PlayOpenAnimation;
        }

        private void ClearSubstriptions()
        {
            _chest.onOpened -= PlayOpenAnimation;
        }
#endregion

    }
}
