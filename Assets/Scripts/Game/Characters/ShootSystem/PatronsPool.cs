using UnityEngine;
using Game.Characters.PoolSystem;
using Game.Characters.Interfaces;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;

namespace Game.Characters.ShootSystem
{
    [Register]
    internal class PatronsPool : Pool, IKernelEntity
    {
        [SerializeField] private int initialCapacity;
        [SerializeField] private GameObject patron;
        private Vector3 _patronStartPosition;

        private protected override IPoolObject CreatePoolObject()
        {
            GameObject newPatron = Instantiate(patron, _patronStartPosition, Quaternion.identity);
            PatronCollisionDetector newPatronCollisionDetector = newPatron.GetComponent<PatronCollisionDetector>();
            IPoolObject patronPoolObject = newPatron.GetComponent<IPoolObject>();
            newPatronCollisionDetector.Initialize(patronPoolObject, this, _gunHandler);
            return patronPoolObject;
        }

#region Construct Field

        [ConstructField]
        private GunHandler _gunHandler;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _patronStartPosition = _gunHandler.transform.position;
            AddObjects(initialCapacity);
        }

#endregion
    }
}
