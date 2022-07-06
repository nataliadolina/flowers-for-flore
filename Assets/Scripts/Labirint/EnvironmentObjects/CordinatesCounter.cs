using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labirint.EnvironmentObjects.Interfaces;

namespace Labirint.EnvironmentObjects
{
    internal class CordinatesCounter : ICoordinatesCounter
    {
        [SerializeField]
        private Vector3 startMazePosition;
        [SerializeField]
        private Vector2 cellSize;

        public Vector2 GetCoordinatesByPosition(Vector3 position)
        {
            Vector3 normalizedPosition = position - startMazePosition;
            return new Vector2((int)normalizedPosition.x/ cellSize.x, (int)normalizedPosition.z / cellSize.y);
        }

        public Vector3 GetPositionByCoordinates(Vector2 coordinates)
        {
            int x = (int)coordinates.x;
            int y = (int)coordinates.y;
            return new Vector3(startMazePosition.x + cellSize.x * x, startMazePosition.y, startMazePosition.z + cellSize.y * y);
        }
    }
}