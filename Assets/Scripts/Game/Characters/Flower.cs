using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Characters.Effects;
using Game.Characters.Interfaces;
using Game.Characters.Abstract;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;


namespace Game.Characters
{
    [Register]
    internal class Flower : ChestEntityPhysics, IChestEntity, IKernelEntity, IScoreManager
    {
        [SerializeField] private float score = 0;

        private GameObject currentMesh = null;
        private Rigidbody flowerRigidbody = null;

        public float Score 
        { get => score;
          set {
                if (value != 0)
                {
                    score += value;
                    ChangeLook();
                }
            }
        }

        public void Initialize()
        {
            
        }
        private void SetMesh()
        {
            if (currentMesh != null)
            {
                Destroy(currentMesh);
            }

            GameObject mesh = _flowersContainer.GetMesh(score);
            currentMesh = Instantiate(mesh, transform.position, Quaternion.identity, transform);
            _flowersContainer.SetFlowerRotation(currentMesh);
        }

        internal void GiveToPlayer()
        {
            _player.Take(this);
        }

        private void ChangeLook()
        {
            if (score <= 0f)
            {
                score = 0f;
                flowerRigidbody.isKinematic = false;
                transform.parent = null;
            }

            SetMesh();
        }

#region KernelEntity

        [ConstructField]
        private FlowersContainer _flowersContainer;

        [ConstructField]
        private Player _player;

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            SetMesh();
        }

#endregion
    }
}
