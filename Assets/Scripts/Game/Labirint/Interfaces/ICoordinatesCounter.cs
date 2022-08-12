using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Labirint.Interfaces
{
    internal interface ICoordinatesCounter
    {
        Vector2 GetCoordinatesByPosition(Vector3 position);

        Vector3 GetPositionByCoordinates(Vector2 coordinates);
    }
}

