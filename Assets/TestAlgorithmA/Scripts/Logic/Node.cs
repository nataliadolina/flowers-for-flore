using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAlgorithmA.Scripts.Logic
{
    internal class Node
    {
        public Vector3 worldPosition { get; set; }
        public Point cell { get; set; }
        public int PathLengthFromStart { get; set; }
        // Точка, из которой пришли в эту точку.
        public Node cameFrom { get; set; }
        // Примерное расстояние до цели (H).
        public int HeuristicEstimatePathLength { get; set; }
        // Ожидаемое полное расстояние до цели (F).
        public int EstimateFullPathLength
        {
            get
            {
                Debug.Log($"{PathLengthFromStart}, {HeuristicEstimatePathLength}");
                Debug.Log($"Full_path = {PathLengthFromStart + HeuristicEstimatePathLength}");
                return PathLengthFromStart + HeuristicEstimatePathLength;
            }
        }

    }

}