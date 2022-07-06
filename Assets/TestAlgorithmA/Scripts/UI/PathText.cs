using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestAlgorithmA.Scripts.UI
{
    internal class PathText : MonoBehaviour
    {
        private Text text = null;

        private void Start()
        {
            text = GetComponent<Text>();
            ChangeEnabled();
        }

        public void ChangeEnabled()
        {
            text.enabled = !text.enabled;
        }
    }
}
