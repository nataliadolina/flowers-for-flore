using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Labirint.Interfaces;
using DI.Attributes.Register;
using DI.Kernel.Interfaces;

namespace Game.Labirint
{
    [Register(typeof(ICoordinatesCounter))]
    internal class CoordinatesCounter : MonoBehaviour, ICoordinatesCounter, IKernelEntity
    {
        [SerializeField]
        private Vector3 startMazePosition = new Vector3(0, 0, 0);
        [SerializeField]
        private Vector2 cellSize = new Vector2(1, 1);

        public Vector2 GetCoordinatesByPosition(Vector3 position)
        {
            Vector3 normalizedPosition = position - startMazePosition;
            return new Vector2((int)(Mathf.Abs(normalizedPosition.x) / cellSize.x), (int)Mathf.Abs(normalizedPosition.z / cellSize.y));
        }

        public Vector3 GetPositionByCoordinates(Vector2 coordinates)
        {
            int x = (int)coordinates.x;
            int y = (int)coordinates.y;
            return new Vector3(startMazePosition.x + cellSize.x * x, startMazePosition.y, startMazePosition.z + cellSize.y * y);
        }
    }
}