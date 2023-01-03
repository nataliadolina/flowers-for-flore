using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Handlers.Abstract;
using DI.Attributes.Run;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;
using Game.Characters;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using System;

namespace Game.Characters.Player
{
    [Register]
    internal class PlayerAnimator : MonoBehaviour, IKernelEntity
    {
        private readonly int Speed = Animator.StringToHash("Speed");
        private readonly int StopWalking = Animator.StringToHash("Stop walking");
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        internal void SetSpeedRatio(float ratio)
        {
            _animator.SetFloat(Speed, ratio);
            if (ratio == 0f)
            {
                _animator.SetTrigger(StopWalking);
            }
        }
    }
}
