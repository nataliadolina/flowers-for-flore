using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TestAlgorithmA.Scripts.MazeGenerators;

namespace TestAlgorithmA.Scripts.Logic
{
    internal class AlgorithmA : MonoBehaviour
    {
        public static List<Point> FindPath(int[,] _field, Point _start, Point _goal)
        {
            var closedSet = new List<Node>();
            var openSet = new List<Node>();

            Node startNode = new Node()
            {
                cell = _start,
                HeuristicEstimatePathLength = GetHeuristicPathLength(_start, _goal),
                PathLengthFromStart = 0,
                cameFrom = null,
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
                print($"current_node = {currentNode.cell.Row}, {currentNode.cell.Col}");
                if (isEquel(currentNode.cell, _goal))
                    return GetPathForNode(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var neighbourNode in GetNeighbours(currentNode, _goal, _field))
                {

                    if (closedSet.Count(node => isEquel(node.cell, neighbourNode.cell)) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      isEquel(node.cell, neighbourNode.cell));

                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                    {
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                        {
                            var cell = openNode.cell;
                            Debug.Log($"Open node cell = {cell.Row}, {cell.Col}");
                            openNode.cameFrom = currentNode;
                            openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        }
                    }
                }
            }
            return null;
        }
        private static bool isEquel(Point cell1, Point cell2)
        {
            return cell1.Col == cell2.Col && cell1.Row == cell2.Row;
        }
        private static int GetDistanceBetweenNeighbours()
        {
            return 1;
        }
        private static int GetHeuristicPathLength(Point from, Point to)
        {
            return Mathf.Abs(from.Row - to.Row) + Mathf.Abs(from.Col - to.Col);
        }
        private static List<Point> GetPathForNode(Node pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.cell);
                currentNode = currentNode.cameFrom;
            }
            result.Reverse();
            return result;
        }
        private static List<Node> GetNeighbours(Node pathNode, Point goal, int[,] field)
        {
            var indices = new (int, int)[4];

            List<Node> neighbourNodes = new List<Node>();
            indices[0] = (pathNode.cell.Row + 1, pathNode.cell.Col);
            indices[1] = (pathNode.cell.Row - 1, pathNode.cell.Col);
            indices[2] = (pathNode.cell.Row, pathNode.cell.Col + 1);
            indices[3] = (pathNode.cell.Row, pathNode.cell.Col - 1);

            foreach (var ind in indices)
            {
                int x = ind.Item1;
                int y = ind.Item2;

                Point point = null;
                try
                {
                    point = MazeConstructor.points[x, y];
                }

                catch
                {
                    continue;
                }

                if (field[x, y] == MazeDataGenerator.empty)
                {
                    var neighbourNode = new Node()
                    {
                        cell = point,
                        cameFrom = pathNode,
                        PathLengthFromStart = pathNode.PathLengthFromStart +
                        GetDistanceBetweenNeighbours(),
                        HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                    };
                    neighbourNodes.Add(neighbourNode);
                }
            }
            return neighbourNodes;
        }
    }
}