using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using System.Collections.Generic;
using DI.Kernel.Enums;
using Game.Characters.Interfaces;

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

        internal void SetFlowerParent(in Transform flowerTransform)
        {
            flowerTransform.parent = transform;
            SetFlowerRotation(flowerTransform);
        }

        private void OnFlowerContactedContainer(IBody flowerBody)
        {
            flowerBody.SetRigidbodiesEnabled(false);
            flowerBody.SetCollisionDetectorsEnabled(false);
            SetFlowerParent(flowerBody.Transform);
        }

#region KernelEntity

        [ConstructMethod(KernelTypeOwner.LogicScene)]
        private void OnConstruct(IKernel kernel)
        {
            SetContactsSubscriptions(kernel.GetInjections<IFlowerTriggerEnterHandler>().ToArray());
        }

#endregion

#region Subscriptions

        private void SetContactsSubscriptions(IFlowerTriggerEnterHandler[] contacts)
        {
            foreach (var contact in contacts)
            {
                contact.onFlowersContainerContact += OnFlowerContactedContainer;
            }
        }

#endregion
    }
}
