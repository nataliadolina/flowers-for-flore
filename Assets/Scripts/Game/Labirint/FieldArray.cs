using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Labirint.Interfaces;
using Game.Labirint.Enums;
using System.Linq;
using DI.Attributes.Construct;
using DI.Attributes.Register;
using DI.Attributes.Run;
using DI.Kernel.Interfaces;
using Utilities.Utils;
using Utilities.Extensions;

namespace Game.Labirint
{
    [Register]
    internal class FieldArray : MonoBehaviour, IKernelEntity
    {
        [SerializeField]
        private List<EntityObjectMap> entityObjectMap;

        [SerializeField]
        private bool getDataFromFile;

        private int[,] fieldArray;
        private Dictionary<Vector2, ILabirintEntity> _entitiesMap = new Dictionary<Vector2, ILabirintEntity>();

        private void BuildLabirint()
        {
            if (getDataFromFile)
            {
                string fieldPath = FileExtensions.GetPath("test.txt");
                fieldArray = fieldPath.StringDataToArray();
                BuildSceneObjectsByArray();
                return;
            }
            BuildArrayBySceneObjects();
        }

        private void BuildSceneObjectsByArray()
        {
            int xDimension = fieldArray.GetLength(0);
            int yDimension = fieldArray.GetLength(1);
            for (int x = 0; x < xDimension; x++)
            {
                for (int y = 0; y < yDimension; y++) {

                    int cell = fieldArray[x, y];
                    Instantiate(entityObjectMap.Where(entity => (int)entity.Key == cell).ToList()[0].Value,
                        _coordinatesCounter.GetPositionByCoordinates(new Vector2(x, y)),
                        Quaternion.identity);
                }
            }
        } 
        
        private void BuildLabirintEntitiesMap(int xDimension, int yDimension)
        {
            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    ILabirintEntity labirintEntity = _labirintEntities.Where(x => x.Coordinates.x == i && x.Coordinates.y == j).ToList<ILabirintEntity>()[0];
                    _entitiesMap.Add(new Vector2(i, j), labirintEntity);
                }
            }
        } 

        private void BuildArrayBySceneObjects()
        {
            int xDimension = (int)_labirintEntities.Max(x => x.Coordinates.x) - (int)_labirintEntities.Min(x => x.Coordinates.x) + 1;
            int yDimension = (int)_labirintEntities.Max(x => x.Coordinates.y) - (int)_labirintEntities.Min(x => x.Coordinates.y) + 1;
            BuildLabirintEntitiesMap(xDimension, yDimension);
            fieldArray = new int[xDimension, yDimension];
            for (int x = 0; x < xDimension; x++)
            {
                for (int y = 0; y < yDimension; y++)
                {
                    if (_entitiesMap.TryGetValue(new Vector2(x, y), out ILabirintEntity labirintEntity)){
                        fieldArray[x, y] = (int)labirintEntity.Entity;
                    }
                }
            }
            Debug.Log(fieldArray);
        }

#region KernelEntity

        [ConstructField]
        private ILabirintEntity[] _labirintEntities;

        [ConstructField]
        private ICoordinatesCounter _coordinatesCounter;

        [RunMethod]
        private void OnRun(IKernel kernel)
        {
            string path = FileExtensions.GetPath("test.txt");
            BuildLabirint();
        }

#endregion
    }
}