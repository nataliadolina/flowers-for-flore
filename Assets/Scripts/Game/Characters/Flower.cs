using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Effects;
using Game.Characters.Interfaces;
using Game.Characters.Enums;
using Game.Characters.Abstract;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using DI.Kernel.Enums;


namespace Game.Characters
{
    [Register]
    internal class Flower : BaseChestEntity, IScoreManager
    {
        [SerializeField] private float score = 0;
        [SerializeField] private List<GameObject> flowerMeshes = null;
        [SerializeField] private float maxScore = 10f;

        private List<float> _scoresNeeded = new List<float>();

#region IScoreManager

        public void SetScoreRange()
        {
            float step = maxScore / flowerMeshes.Count;

            for (int i = 0; i < flowerMeshes.Count; i++)
            {
                _scoresNeeded.Add(step * i);
            }
        }

        public float Score 
        { 
            get => score;
            set
            {
                if (value != 0)
                {
                    score += value;
                    ChangeLook();
                }
            }
        }

        public GameObject GetLook(float score)
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

#endregion

        private void OnFlowerAppear()
        {
            IsActive = true;
        }

        private void SetMesh()
        {
            if (_currentMesh != null)
            {
                Destroy(_currentMesh);
            }

            GameObject look = GetLook(score);
            _currentMesh = Instantiate(look, transform.position, Quaternion.identity, transform);
        }

        private void ChangeLook()
        {
            if (score <= 0f)
            {
                score = 0f;
                _chestEntityBody.SetRigidbodiesEnabled(true);
                transform.parent = null;
            }

            SetMesh();
        }

#region KernelEntity

        [ConstructField]
        private IChestAnimator _chestAnimator;

        private IBody _chestEntityBody;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _chestEntityBody = kernel.GetInjection<IBody>(x => x.OwnerType == OwnerType.ChestEntity);
        }

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            SetMesh();
            IsActive = false;
            _chestAnimator.onOpenAnimationStoppedPlaying += () => IsActive = true;
        }

#endregion
    }
}
