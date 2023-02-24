using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using System;
using Game.Characters.Handlers;

namespace Game.Characters.ShootSystem
{
    [Register]
    [Register(typeof(ICausePlayerDamage))]
    internal sealed class GunHandler : MonoBehaviour, ICausePlayerDamage, IKernelEntity 
    {
        public event Action<float> onCausedPlayerDamage;

        [SerializeField] private float harm;

        internal void Shoot()
        {
            IPoolObject poolObject = _patronPool.Take();
            poolObject.Activate();
        }

        internal void CausePlayerDamage()
        {
            onCausedPlayerDamage?.Invoke(harm);
        }

#region Kernel Entity

        [ConstructField]
        private PatronsPool _patronPool;

#endregion
    }
}
