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
    [Register(typeof(IChestEntity))]
    internal class Flower : ChestEntityPhysics, IChestEntity, IKernelEntity, IScoreManager
    {
        [SerializeField] private float score = 0;

        private GameObject _currentMesh = null;

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    _currentMesh.SetActive(value);
                }
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
        

        public void Initialize()
        {
            
        }

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

            GameObject mesh = _flowersContainer.GetMesh(score);
            _currentMesh = Instantiate(mesh, transform.position, Quaternion.identity, transform);
            _isActive = false;
        }

        internal void GiveToPlayer()
        {
            _flowersContainer.SetFlowerRotation(_currentMesh);
            _player.Take(this);
        }

        private void ChangeLook()
        {
            if (score <= 0f)
            {
                score = 0f;
                SetRigidbodiesEnabled(true);
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
            SetMesh();
        }

#endregion
    }
}
