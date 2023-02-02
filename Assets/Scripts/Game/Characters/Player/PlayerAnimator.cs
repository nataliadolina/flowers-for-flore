using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        private readonly int StopMoving = Animator.StringToHash("Stop moving");
        
        private Animator _animator;

        private float _speedRatio;
        private float SpeedRatio
        {
            get => _speedRatio;
            set
            {
                if (_speedRatio != value)
                {
                    _speedRatio = value;
                    _animator.SetFloat(Speed, value);

                    if (value == 0)
                    {
                        _animator.SetTrigger(StopMoving);
                    }
                }
            }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        internal void SetSpeedRatio(float ratio)
        {
            SpeedRatio = ratio;
        }
    }
}
