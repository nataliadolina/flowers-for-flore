using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labirint.EnvironmentObjects.Interfaces;
using Labirint.EnvironmentObjects.Enums;

namespace Labirint.EnvironmentObjects.Base
{
    internal abstract class BaseLabirintEntity : MonoBehaviour, ILabirintEntity
    {
        [SerializeField] private Entity entity;

        public Entity Entity { get => entity; }
        public Vector2 Coordinates { get => _coordinates; }

        private Vector2 _coordinates;

        private void Awake()
        {
            _coordinates = _coordinatesCounter.GetCoordinatesByPosition(transform.position);
        }

#region Kernel Entity

        private ICoordinatesCounter _coordinatesCounter;

#endregion
    }
}
