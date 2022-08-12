using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using System;

namespace Game.Characters
{
    [Register]
    internal class ChestsController : MonoBehaviour
    {
        private IChest _currentChest;
        internal IChest CurrentChest
        {
            get => _currentChest;
            set
            {
                _currentChest = value;
                Array.ForEach(_chests, x => x.IsSelected = true ? x.Equals(value) : false);
            }
        }

#region Kernel Entity

        [ConstructField]
        private IChest[] _chests;

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            Array.ForEach(_chests, x => x.IsVisible = true);
        }

#endregion
    }
}
