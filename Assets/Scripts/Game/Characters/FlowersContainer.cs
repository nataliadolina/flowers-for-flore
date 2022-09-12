using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;

namespace Game.Characters
{
    [Register]
    internal class FlowersContainer : MonoBehaviour, IKernelEntity
    {
        [SerializeField] private List<GameObject> flowerMeshes = null;
        [SerializeField] private float maxScore = 10f;

        private Transform[] _flowerTransforms;
        private List<float> _scoresNeeded = new List<float>();

        private int _lastRotInd = 0;

        private void Start()
        {
            _flowerTransforms = GetComponentsInChildren<Transform>();
            SetScoreRange();
        }

        private void SetScoreRange()
        {
            float step = maxScore / flowerMeshes.Count;

            for (int i = 0; i < flowerMeshes.Count; i++)
            {
                _scoresNeeded.Add(step * i);
            }
        }

        internal void SetFlowerRotation(GameObject flower)
        {
            if (_lastRotInd >= _flowerTransforms.Length)
            {
                _lastRotInd = 0;
            }

            Transform pos = _flowerTransforms[_lastRotInd];

            flower.transform.rotation = pos.rotation;
            flower.transform.position = pos.position;
            _lastRotInd++;
        }

        internal GameObject GetMesh(float score)
        {
            for (int i = 0; i < _scoresNeeded.Count - 1; i++)
            {
                if (score >= _scoresNeeded[i] & score < _scoresNeeded[i + 1])
                {
                    return flowerMeshes[i];
                }
            }

            return flowerMeshes[flowerMeshes.Count - 1];
        }
    }
}
