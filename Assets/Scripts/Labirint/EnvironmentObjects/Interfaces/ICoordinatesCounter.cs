using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Labirint.EnvironmentObjects.Interfaces
{
    internal interface ICoordinatesCounter
    {
        Vector2 GetCoordinatesByPosition(Vector3 position);

        Vector3 GetPositionByCoordinates(Vector2 coordinates);
    }
}

