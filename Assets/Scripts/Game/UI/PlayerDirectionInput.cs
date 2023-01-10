using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.UI.Abstract;
using Game.UI.Interfaces;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;

namespace Game.UI
{
    [Register(typeof(IPlayerDirectionInput))]
    internal class PlayerDirectionInput : JoystickAreaHandler, IPlayerDirectionInput, IKernelEntity
    {
        /// <summary>
        /// Vector3: direction
        /// </summary>
        public event Action<Vector2, bool> onCharacterDirectionChanged;

        private protected override void UpdateDirection(in Vector2 direction, in bool updateDirectionInProgress)
        {
            onCharacterDirectionChanged?.Invoke(direction, updateDirectionInProgress);
        }
    }
}
