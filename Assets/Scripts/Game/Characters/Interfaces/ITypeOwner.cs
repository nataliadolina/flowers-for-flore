using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Enums;
using System;

namespace Game.Characters.Interfaces
{
    internal interface IOwnerType
    {
        OwnerType OwnerType { get; }
    }
}