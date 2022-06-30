using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TestAlgorithmA.Scripts.Logic;

namespace TestAlgorithmA.Scripts.UI
{
    internal class RegimeButton : MonoBehaviour
    {
        [SerializeField] private Text text;

        private Pointer pointer = null;

        void Start()
        {
            pointer = FindObjectOfType<Pointer>();
            SetText();
        }

        public void SetText()
        {
            string name = FindObjectOfType<Pointer>().CurrentRuntime.name;
            text.text = "Текущий режим: " + name;
        }
    }

}