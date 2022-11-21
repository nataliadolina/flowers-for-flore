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
using DI.Kernel.Enums;


namespace Game.Characters
{
    [Register]
    [Register(typeof(ChestEntityPhysics))]
    internal class Flower : ChestEntityPhysics, IChestEntity, IKernelEntity, IScoreManager
    {
        [SerializeField] private float score = 0;

        private GameObject currentMesh = null;

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
        }

        internal void GiveToPlayer()
        {
            _flowersContainer.SetFlowerRotation(currentMesh);
            _player.Take(this);
        }

        private void ChangeLook()
        {
            if (score <= 0f)
            {
                score = 0f;
                SetRigidbodiesEnabled(false);
                transform.parent = null;
            }

            SetMesh();
        }

#region KernelEntity

        [ConstructField(KernelTypeOwner.Player)]
        private FlowersContainer _flowersContainer;

        [ConstructField(KernelTypeOwner.Player)]
        private Player _player;

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            Debug.Log(_player);
            Debug.Log(_flowersContainer);
            SetMesh();
        }

#endregion
    }
}
