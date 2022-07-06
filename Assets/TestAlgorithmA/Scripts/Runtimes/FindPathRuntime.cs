using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestAlgorithmA.Scripts.Logic;
using TestAlgorithmA.Scripts.MazeGenerators;

namespace TestAlgorithmA.Scipts.Runtimes
{
    internal class FindPathRuntime : Runtime
    {
        private Point[] fromTo = null;
        private int currentIndex = 0;

        private List<Point> previousWay = null;

        private void Awake()
        {
            name = "Генерация пути";
        }

        private void Start()
        {
            fromTo = new Point[2];
            previousWay = new List<Point>();
        }

        public override void Click(Transform aim)
        {
            SelectStartAndFinishPoint(aim);
        }

        public override void Run()
        {
            GenerateWay();
        }

        private void SelectStartAndFinishPoint(Transform aim)
        {
            ResetSelection();
            var cell = PosNum.ReturnObjectCell(aim.position);

            int row = cell.Item1;
            int col = cell.Item2;

            var typeCell = MazeConstructor.data[row, col];
            if (typeCell == MazeDataGenerator.empty)
            {
                var point = MazeConstructor.points[row, col];

                var previous_sel = fromTo[currentIndex % 2];
                if (previous_sel != null)
                    previous_sel.GetObj.ResetSelection();

                fromTo[currentIndex % 2] = point;
                point.GetObj.Select();
                currentIndex++;
            }
        }

        private void GenerateWay()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var from = fromTo[0];
                var to = fromTo[1];
                if (from != null & to != null)
                {
                    previousWay = AlgorithmA.FindPath(MazeConstructor.data, from, to);
                    if (previousWay != null)
                    {
                        foreach (Point p in previousWay)
                        {
                            p.GetObj.Tread();
                        }
                    }
                }
            }
        }

        private void ResetSelection()
        {
            if (previousWay != null)
            {
                foreach (var p in previousWay)
                {
                    if (p != null)
                        p.GetObj.ResetSelection();
                }
            }
        }

        public void ClearPoints()
        {
            ResetSelection();
            previousWay = null;
            for (int i = 0; i < 2; i++)
            {
                var p = fromTo[i];
                if (p != null)
                {
                    p.GetObj.ResetSelection();
                    fromTo[i] = null;
                }
            }
        }
    }
}
