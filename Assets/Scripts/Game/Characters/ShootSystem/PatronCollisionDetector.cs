using UnityEngine;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;
using DI.Attributes.Run;
using Game.Characters.Player;
using Game.Characters.PoolSystem;
using Game.Characters.Interfaces;

namespace Game.Characters.ShootSystem
{
    [Register]
    internal class PatronCollisionDetector : MonoBehaviour, IKernelEntity
    {
        private Pool _patronsPool;
        private IPoolObject _poolObject;
        private GunHandler _gunHandler;

        internal void Initialize(IPoolObject poolObject, Pool patronsPool, GunHandler gunHandler)
        {
            _poolObject = poolObject;
            _patronsPool = patronsPool;
            _gunHandler = gunHandler;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collider collider = collision.collider;
            if (collider.GetComponent<PlayerMovement>())
            {
                _gunHandler.CausePlayerDamage();
            }

            _patronsPool.Release(_poolObject);
        }
    }
}
