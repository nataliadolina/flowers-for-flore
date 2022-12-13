using System;

namespace Game.Characters.Interfaces
{
    internal interface IChestAnimator
    {
        event Action onOpenAnimationStoppedPlaying;
    }
}
