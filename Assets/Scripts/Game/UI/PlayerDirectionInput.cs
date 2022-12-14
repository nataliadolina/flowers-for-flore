using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.UI.Abstract;

namespace Game.UI
{
    internal class PlayerDirectionInput : JoystickAreaHandler
    {
        /// <summary>
        /// Vector3: direction
        /// </summary>
        public Action<Vector3> onCharacterDirectionChanged;

        private protected override void UpdateDirection(in Vector3 direction)
        {
            onCharacterDirectionChanged?.Invoke(new Vector3(direction.x, 0, direction.y));
        }
    }
}
