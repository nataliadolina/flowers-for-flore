using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAlgorithmA.Scipts.Runtimes
{
    internal abstract class Runtime : MonoBehaviour
    {
        [HideInInspector] public string name;

        public abstract void Click(Transform aim);

        public virtual void Run()
        {

        }
    }
}
