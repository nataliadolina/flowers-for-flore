using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Utils;

namespace Game.Utilities.Behaviours
{
    internal abstract class MultiThreadedUpdateMonoBehaviour : MonoBehaviour
    {
        private protected List<InvokeRepeatingSettings> _updateThreads;
        private protected abstract void SetUpUpdateSettings();

        private protected void StartInternal()
        {
            SetUpUpdateSettings();
            StartUpdate();
        }

        private void StartUpdate()
        {
            foreach (var updateThread in _updateThreads)
            {
                updateThread.Invoke(this);
            }
        }

        private void OnDestroy()
        {
            foreach (var updateThread in _updateThreads)
            {
                updateThread.Cancel(this);
            }
        }
    }
}
