using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Utilities.Extensions
{
    internal static class FileExtensions
    {
        private const string ROOT_DIRECTORY = "Assets\\Files";
        private static readonly Dictionary<char, int> Notation = new Dictionary<char, int>()
        {
            { '0', 0},
            { '1', 1},
        };
        internal static string GetPath(params string[] paths)
        {
            string currentDirectory = Path.Combine(ROOT_DIRECTORY, paths[0]);
            for (int i = 1; i < paths.Length; i++)
            {
                currentDirectory = Path.Combine(currentDirectory, paths[i]);
            }
            return currentDirectory;
        }
        internal static int[,] StringDataToArray(this string path)
        {
            String[] data = File.ReadAllText(path).Split('\n');
            int xDimension = data.Length;
            int yDimension = data[0].Length - 1;

            int[,] result = new int[xDimension, yDimension];
            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    int symbol = Notation[data[i][j]];
                    result[i, j] = symbol;
                }
            }
            return result;
        } 
    }
}