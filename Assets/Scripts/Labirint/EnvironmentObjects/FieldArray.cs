using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labirint.EnvironmentObjects.Interfaces;
using Labirint.EnvironmentObjects.Enums;
using System.Linq;

namespace Labirint.EnvironmentObjects
{
    internal class FieldArray : MonoBehaviour
    {
        private int[,] _fieldArray;

        private void BuildArray()
        {
            int xDimension = (int)_labirintEntities.Max(x => x.Coordinates.x);
            int yDimension = (int)_labirintEntities.Max(x => x.Coordinates.x);
            _fieldArray = new int[xDimension, yDimension];
            for (int x=0; x < xDimension; x++)
            {
                for (int y = 0; y < yDimension; y++)
                {
                    _fieldArray[x, y] = (int)_labirintEntities.Where(z => z.Coordinates.x == x
                    && z.Coordinates.y == y).ToList()[0].Entity;
                }
            }
        }

#region KernelEntity

        private ILabirintEntity[] _labirintEntities;

#endregion

    }
}