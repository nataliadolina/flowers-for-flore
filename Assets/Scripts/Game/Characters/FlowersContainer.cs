using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;

namespace Game.Characters
{
    [Register]
    internal class FlowerContainer : MonoBehaviour, IKernelEntity
    {
        private Transform[] _flowerTransforms;

        private int _lastRotInd = 0;

        private void Start()
        {
            _flowerTransforms = GetComponentsInChildren<Transform>();
        }

        private void SetFlowerRotation(Transform flowerTransform)
        {
            if (_lastRotInd >= _flowerTransforms.Length)
            {
                _lastRotInd = 0;
            }

            Transform pos = _flowerTransforms[_lastRotInd];

            flowerTransform.rotation = pos.rotation;
            flowerTransform.position = pos.position;
            _lastRotInd++;
        }

        internal void SetFlowerParent(Transform flowerTransform)
        {
            flowerTransform.parent = transform;
            SetFlowerRotation(flowerTransform);
        }
    }
}
