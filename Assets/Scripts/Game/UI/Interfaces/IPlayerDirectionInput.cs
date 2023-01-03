using UnityEngine;
using System;

namespace Game.UI.Interfaces
{
    internal interface IPlayerDirectionInput
    {
        event Action<Vector2> onCharacterDirectionChanged;
    }
}