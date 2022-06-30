using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAlgorithmA.Scripts.Logic
{
    internal class Point : MonoBehaviour
    {
        [SerializeField] private PointObj obj;
        private int row;
        private int col;

        private int type = 0;

        internal PointObj GetObj
        {
            get { return obj; }
            private set { obj = value; }
        }

        internal int Row
        {
            get { return row; }
        }

        internal int Col
        {
            get { return col; }
        }

        internal (int, int) Cell
        {
            set { row = value.Item1; col = value.Item2; }
        }

        private void OnEnable()
        {
            obj = GetComponentInChildren<PointObj>();
            Cell = PosNum.ReturnObjectCell(transform.position);
        }
    }
}
