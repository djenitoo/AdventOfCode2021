using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Program
    {
        public static List<int[]> RiskLevelMap = new List<int[]>();

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
        }

        private static long CurrentMinimalPath = long.MaxValue;
        private static int RowLength = 0;

        private static long TaskOne()
        {
            long result = 0;
            RowLength = RiskLevelMap[0].Length;

            List<List<int[]>> possiblePaths = new List<List<int[]>>();
            var basicPathRiskLevel = (RiskLevelMap[0].Sum() + RiskLevelMap.Select(x => x[RowLength - 1]).Sum()) - RiskLevelMap[0][0];
            CurrentMinimalPath = basicPathRiskLevel;

            //GetPossiblePaths(possiblePaths, new List<int[]> { new int[] { 0, 0 } }, 0, 1);
            //GetPossiblePaths(possiblePaths, new List<int[]> { new int[] { 0, 0 } }, 1, 0);

            GetPossiblePaths(new List<int[]>(), 0 - RiskLevelMap[0][0], 0, 0);


            result = CurrentMinimalPath;

            return result;
        }

        private static void GetPossiblePaths(List<int[]> currentPath, long currentTotalRisk, int row, int col)
        {
            if (IsOutOfBounds(row, col))
            {
                return;
            }

            List<int[]> newCurrentPath = new List<int[]>();
            newCurrentPath.AddRange(currentPath);
            newCurrentPath.Add(new int[] { row, col });
            currentTotalRisk += RiskLevelMap[row][col];

            if (IsFinalPoint(row, col))
            {
                // count total and compare with minimal path count
                // add to possiblePaths
                long currentPathRiskLevel = currentTotalRisk;

                if (CurrentMinimalPath > currentPathRiskLevel)
                {
                    CurrentMinimalPath = currentPathRiskLevel;
                }

                //possiblePaths.Add(newCurrentPath);

                return;
            }

            if (IsAlreadyVisitedPoint(currentPath, row, col) || currentTotalRisk >= CurrentMinimalPath)
            {
                //possiblePaths.Add(newCurrentPath);
                return;
            }

            GetPossiblePaths(newCurrentPath, currentTotalRisk, row + 1, col);
            GetPossiblePaths(newCurrentPath, currentTotalRisk, row, col + 1);
            GetPossiblePaths(newCurrentPath, currentTotalRisk, row - 1, col);
            GetPossiblePaths(newCurrentPath, currentTotalRisk, row, col - 1);
            
        }

        private static bool IsRepeatingPath(List<List<int[]>> possiblePaths, List<int[]> currentPath)
        {
            bool result = false;

            //string currentPathStr = string.Join(", ", currentPath.Select(c => c[0] + "-" + c[1]));
            var newCPath = currentPath.Select(x => x[0] + "-" + x[1]).ToList();
            foreach (var pPath in possiblePaths)
            {
                var newPPath = pPath.Select(x => x[0] + "-" + x[1]).ToList();
                //if (currentPathStr.Equals(string.Join(", ", pPath.Select(c => c[0] + "-" + c[1]))))
                if (newCPath.All(c => newPPath.Contains(c)))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private static bool IsAlreadyVisitedPoint(List<int[]> currentPath, int row, int col)
        {
            return currentPath.Any(p => p[0] == row && p[1] == col);
        }

        private static bool IsOutOfBounds(int row, int col)
        {
            bool isOutOfBounds = false;

            if (row < 0 || row >= RiskLevelMap.Count)
            {
                isOutOfBounds = true;
            }

            if (col < 0 || col >= RowLength)
            {
                isOutOfBounds = true;
            }

            return isOutOfBounds;
        }

        private static bool IsFinalPoint(int row, int col)
        {
            return (row == RiskLevelMap.Count - 1) && (col == RowLength - 1);
        }

        private static long GetPathTotalRisk(List<int[]> path)
        {
            long total = 0;
            int substract = RiskLevelMap[0][0];

            foreach (var coord in path)
            {
                total += RiskLevelMap[coord[0]][coord[1]];
            }

            return total - substract;
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                int[] currentRiskLevelRow = inputLine
                    .ToCharArray()
                    .Select(x => int.Parse(x.ToString()))
                    .ToArray();

                RiskLevelMap.Add(currentRiskLevelRow);

                inputLine = Console.ReadLine();
            }
        }
    }
}
