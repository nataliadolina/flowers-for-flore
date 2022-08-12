using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Labirint.Interfaces;
using Game.Labirint.Enums;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;
using DI.Attributes.Construct;

namespace Game.Labirint.Base
{
    [Register(typeof(ILabirintEntity))]
    internal class BaseLabirintEntity : MonoBehaviour, ILabirintEntity, IKernelEntity
    {
        [SerializeField] private Entity entity;

        public Entity Entity { get => entity; }
        public Vector2 Coordinates { get => _coordinates; }

        private Vector2 _coordinates;

#region Kernel Entity

        [ConstructField]
        private ICoordinatesCounter _coordinatesCounter;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _coordinates = _coordinatesCounter.GetCoordinatesByPosition(transform.position);
        }

#endregion
    }
}
