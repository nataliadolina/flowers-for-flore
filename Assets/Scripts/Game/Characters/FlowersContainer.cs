using UnityEngine;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using System.Collections.Generic;

namespace Game.Characters
{
    [Register]
    internal class FlowersContainer : MonoBehaviour, IKernelEntity
    {
        [SerializeField]
        private Transform[] _flowerTransforms;

        private int _lastRotInd = 0;

        private void SetFlowerRotation(Transform flowerTransform)
        {
            if (_lastRotInd >= _flowerTransforms.Length)
            {
                _lastRotInd = 0;
            }

            Transform transformValues = _flowerTransforms[_lastRotInd];
            Vector3 rotationValues = transformValues.eulerAngles;
            Vector3 positionValues = transformValues.position;
            Vector3 flowerCurrentRotation = flowerTransform.eulerAngles;

            flowerTransform.Rotate(-flowerCurrentRotation.x + rotationValues.x, 0, flowerCurrentRotation.z + rotationValues.z);
            flowerTransform.position = positionValues;
            _lastRotInd++;
        }

        internal void SetFlowerParent(Transform flowerTransform)
        {
            flowerTransform.parent = transform;
            SetFlowerRotation(flowerTransform);
        }
    }
}
