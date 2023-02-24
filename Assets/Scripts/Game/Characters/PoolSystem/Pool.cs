using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Interfaces;
using System.Linq;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;

namespace Game.Characters.PoolSystem
{
    internal abstract class Pool : MonoBehaviour
    {
        private List<IPoolObject> _freePoolObjects = new List<IPoolObject>();

        private protected abstract IPoolObject CreatePoolObject();

        internal void AddObjects(int length)
        {
            for (int i = 0; i < length; i++)
            {
                IPoolObject poolObject = CreatePoolObject();
                _freePoolObjects.Add(poolObject);
            }
        }

        internal IPoolObject Take()
        {
            if (_freePoolObjects.Count == 0)
            {
                AddObjects(1);
            }

            IPoolObject poolObject = _freePoolObjects.FirstOrDefault();
            _freePoolObjects.Remove(poolObject);
            return poolObject;
        }

        internal void Release(IPoolObject poolObject)
        {
            poolObject.ResetPoolObject();
            _freePoolObjects.Add(poolObject);
        }
    }
}
