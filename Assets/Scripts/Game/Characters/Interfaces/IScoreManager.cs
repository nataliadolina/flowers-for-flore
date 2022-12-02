using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Interfaces
{
    internal interface IScoreManager
    {
        void SetScoreRange();

        GameObject GetLook(float score);

        float Score { get; }
    }
}