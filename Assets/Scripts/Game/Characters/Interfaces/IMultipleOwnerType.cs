using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Enums;

namespace Game.Characters.Interfaces
{
    internal interface IMultipleOwnerType
    {
        public OwnerTypeFlags OwnerTypes { get; }
    }
}
