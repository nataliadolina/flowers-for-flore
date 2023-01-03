using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Game.Settings
{
    internal class ApplyGameSettings : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = GameParams.FPS;
        }
    }
}

