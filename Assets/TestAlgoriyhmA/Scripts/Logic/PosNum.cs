using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAlgorithmA.Scripts.Logic
{
    internal class PosNum : MonoBehaviour
    {
        [SerializeField] private Transform mazeStartTr;
        [SerializeField] private Transform deltaTr;

        private static Vector3 mazeStart;
        private static float delta;

        private void Awake()
        {
            mazeStart = mazeStartTr.position;
            delta = deltaTr.localScale.x;
        }

        public static (int, int) ReturnObjectCell(Vector3 vector)
        {
            int col = Mathf.RoundToInt((vector.x - mazeStart.x) / delta);
            int row = Mathf.RoundToInt((vector.z - mazeStart.z) / delta);
            return (row, col);
        }

        public static Vector3 ReturnPositionInMaze(int row, int col)
        {
            return new Vector3(mazeStart.x + delta * col, mazeStart.y, mazeStart.z + delta * row);
        }
    }
}
