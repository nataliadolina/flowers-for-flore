using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAlgorithmA.Scripts.MazeGenerators
{
    internal class MazeDataGenerator : MonoBehaviour
    {
        [SerializeField] private int mazeHeight = 0;
        [SerializeField] private int mazeWidth = 0;

        [SerializeField] private float placementThreshold; // вероятность свободного места

        public int[,] maze;

        public static int wall = 1;
        public static int empty = 0;

        private void Start()
        {
            placementThreshold = .1f;
        }

        public int[,] Generate()
        {
            maze = new int[mazeHeight, mazeWidth];
            int rMax = maze.GetUpperBound(0) - 1;
            int cMax = maze.GetUpperBound(1) - 1;

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (i == 0 || j == 0 || i == rMax || j == cMax)
                    {
                        maze[i, j] = 0;
                    }

                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        if (UnityEngine.Random.value > placementThreshold)
                        {
                            maze[i, j] = wall;

                            int a = UnityEngine.Random.value < .5 ? empty : 1;
                            int b = a != 0 ? empty : (UnityEngine.Random.value < .5 ? -1 : 1);
                            maze[i + a, j + b] = wall;
                        }
                    }
                }
            }
            return maze;
        }

        private int GenerateIndex(List<int> generated, int minIndex, int maxIndex)
        {
            int index = UnityEngine.Random.Range(minIndex, maxIndex);
            while (generated.Contains(index))
            {
                index = UnityEngine.Random.Range(0, maxIndex);
            }
            return index;
        }
    }
}
