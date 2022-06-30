using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestAlgorithmA.Scripts.Logic;
using TestAlgorithmA.Scripts.MazeGenerators;

namespace TestAlgorithmA.Scipts.Runtimes
{
    internal class MapChangerRuntime : Runtime
    {
        private Transform parent;

        private void Awake()
        {
            name = "Редактирование карты";
        }

        private void Start()
        {
            parent = FindObjectOfType<MazeConstructor>().transform;
        }

        public override void Click(Transform aim)
        {
            var cell = PosNum.ReturnObjectCell(aim.position);

            int row = cell.Item1;
            int col = cell.Item2;

            var obj = MazeConstructor.points[row, col].gameObject;
            var pos = obj.transform.position;

            Destroy(obj);
            int type = MazeConstructor.data[row, col];

            int key = MazeDataGenerator.empty;
            if (type == MazeDataGenerator.empty)
            {
                key = MazeDataGenerator.wall;

            }

            MazeConstructor.data[row, col] = key;

            var finalObj = MazeConstructor.nodesObjects[key].gameObject;
            var point = Instantiate(finalObj, pos, Quaternion.identity, parent);
            MazeConstructor.points[row, col] = point.GetComponent<Point>();
        }
    }

}