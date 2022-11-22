using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using Game.Characters.Interfaces;
using System;

namespace Game.Characters
{
    [Register]
    [Register(typeof(IBody))]
    internal class Player : MonoBehaviour, IKernelEntity, IScoreManager
    {
        [SerializeField] private float score = 0;

        public float Score
        {
            get => score;
        }

        public float HitPoint
        {
            set
            {
                score += value;
                ChangeLook(value);
            }
        }

        internal void Take(IChestEntity _chestEntity)
        {

        }

        private void ChangeLook(float damage) { }
    }
}
