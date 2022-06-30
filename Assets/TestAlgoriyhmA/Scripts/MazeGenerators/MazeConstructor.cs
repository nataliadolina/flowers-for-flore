using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestAlgorithmA.Scripts.Logic;


namespace TestAlgorithmA.Scripts.MazeGenerators
{
    internal class MazeConstructor : MonoBehaviour
    {
        [SerializeField] private Point wall = null;
        [SerializeField] private Point empty = null;

        private MazeDataGenerator dataGenerator = null;

        public static Dictionary<int, Point> nodesObjects;

        public static Point[,] points;

        public static int[,] data;

        private void Start()
        {
            dataGenerator = FindObjectOfType<MazeDataGenerator>();

            nodesObjects = new Dictionary<int, Point>
        {
            { MazeDataGenerator.wall, wall},
            { MazeDataGenerator.empty, empty}

        };
            data = new int[,]
            {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
            };

            data = dataGenerator.Generate();
            points = new Point[data.GetUpperBound(0), data.GetUpperBound(1)];
            BuildMaze();
        }

        private void BuildMaze()
        {
            int num_rows = data.GetUpperBound(0);
            int num_cols = data.GetUpperBound(1);
            for (int row = 0; row < num_rows; row++)
            {
                for (int col = 0; col < num_cols; col++)
                {
                    Point point = nodesObjects[data[row, col]];

                    var pos = PosNum.ReturnPositionInMaze(row, col);
                    var p = Instantiate(point.gameObject, pos, Quaternion.identity, transform);
                    points[row, col] = p.GetComponent<Point>();
                }
            }
        }
    }
}
