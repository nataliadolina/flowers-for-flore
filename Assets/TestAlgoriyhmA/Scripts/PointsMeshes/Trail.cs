using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAlgorithmA.Scripts.Logic
{
    internal class Trail : PointObj
    {
        [SerializeField] private MeshRenderer renderer = null;


        [SerializeField] private Material tread = null;
        [SerializeField] private Material selected = null;

        private Material initialMat;

        private void OnEnable()
        {
            renderer = GetComponent<MeshRenderer>();
            initialMat = renderer.material;
        }

        public override void Select()
        {
            renderer.material = selected;
        }

        public override void Tread()
        {
            renderer.material = tread;
        }

        public override void ResetSelection()
        {
            renderer.material = initialMat;
        }
    }
}
