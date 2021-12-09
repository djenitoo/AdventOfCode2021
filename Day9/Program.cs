using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        public static List<int[]> InputHeatmap = new List<int[]>();

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;
            List<int> lowPoints = new List<int>();
            int rowLength = InputHeatmap[0].Length;

            for (int i = 0; i < InputHeatmap.Count; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    int upIndex = i > 0 ? i - 1 : -1;
                    int downIndex = i + 1 < InputHeatmap.Count ? i + 1 : -1;
                    int leftIndex = j > 0 ? j - 1 : -1;
                    int rightIndex = j + 1 < rowLength ? j + 1 : -1;

                    List<int> compareValues = new List<int>();

                    if (upIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[upIndex][j]);
                    }
                    if (downIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[downIndex][j]);
                    }
                    if (leftIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[i][leftIndex]);
                    }
                    if (rightIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[i][rightIndex]);
                    }

                    int currentPoint = InputHeatmap[i][j];

                    bool isLowPoint = true;

                    for (int k = 0; k < compareValues.Count; k++)
                    {
                        if (currentPoint >= compareValues[k])
                        {
                            isLowPoint = false;
                            break;
                        }
                    }

                    if (isLowPoint)
                    {
                        lowPoints.Add(currentPoint);
                    }
                }
            }

            if (lowPoints.Any())
            {
                lowPoints.ForEach(l => result += (l + 1));
            }

            return result;
        }

        private static long TaskTwo()
        {
            long result = 1;
            List<int> basinSizes = new List<int>();

            var lowPointsCoordinates = GetLowPoints();

            foreach (var lpCoord in lowPointsCoordinates)
            {
                var emptyBasin = new List<int[]>();

                for (int i = 0; i < InputHeatmap.Count; i++)
                {
                    emptyBasin.Add(new int[InputHeatmap[0].Length]);
                }

                emptyBasin[lpCoord[0]][lpCoord[1]] = 1;
                emptyBasin = FindBasin(emptyBasin, lpCoord[0], lpCoord[1]);
                
                int basinSize = 0;

                emptyBasin.ForEach(r => basinSize += r.Where(it => it != 0).Count());

                basinSizes.Add(basinSize);
            }

            basinSizes.OrderByDescending(x => x).Take(3).ToList().ForEach(n => result *= n);

            return result;
        }

        private static List<int[]> FindBasin(List<int[]> emptyBasin, int row, int col)
        {
            int currentPoint = InputHeatmap[row][col];
            int leftPoint = col != 0 ? InputHeatmap[row][col - 1] : 9;

            if (leftPoint != 9 && emptyBasin[row][col - 1] != 1 && leftPoint > currentPoint)
            {
                emptyBasin[row][col - 1] = 1;
                emptyBasin = FindBasin(emptyBasin, row, col - 1);
            }

            int rightPoint = col + 1 < emptyBasin[0].Length ? InputHeatmap[row][col + 1] : 9;

            if (rightPoint != 9 && emptyBasin[row][col + 1] != 1 && rightPoint > currentPoint)
            {
                emptyBasin[row][col + 1] = 1;
                emptyBasin = FindBasin(emptyBasin, row, col + 1);
            }

            int topPoint = row - 1 >= 0 ? InputHeatmap[row - 1][col] : 9;

            if (topPoint != 9 && emptyBasin[row - 1][col] != 1 && topPoint > currentPoint)
            {
                emptyBasin[row - 1][col] = 1;
                emptyBasin = FindBasin(emptyBasin, row - 1, col);
            }

            int bottomPoint = row + 1 < emptyBasin.Count ? InputHeatmap[row + 1][col] : 9;

            if (bottomPoint != 9 && emptyBasin[row + 1][col] != 1 && bottomPoint > currentPoint)
            {
                emptyBasin[row + 1][col] = 1;
                emptyBasin = FindBasin(emptyBasin, row + 1, col);
            }

            return emptyBasin;
        }

        private static List<int[]> GetLowPoints()
        {
            var result = new List<int[]>();

            int rowLength = InputHeatmap[0].Length;

            for (int i = 0; i < InputHeatmap.Count; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    int upIndex = i > 0 ? i - 1 : -1;
                    int downIndex = i + 1 < InputHeatmap.Count ? i + 1 : -1;
                    int leftIndex = j > 0 ? j - 1 : -1;
                    int rightIndex = j + 1 < rowLength ? j + 1 : -1;

                    List<int> compareValues = new List<int>();

                    if (upIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[upIndex][j]);
                    }
                    if (downIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[downIndex][j]);
                    }
                    if (leftIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[i][leftIndex]);
                    }
                    if (rightIndex > -1)
                    {
                        compareValues.Add(InputHeatmap[i][rightIndex]);
                    }

                    int currentPoint = InputHeatmap[i][j];

                    bool isLowPoint = true;

                    for (int k = 0; k < compareValues.Count; k++)
                    {
                        if (currentPoint >= compareValues[k])
                        {
                            isLowPoint = false;
                            break;
                        }
                    }

                    if (isLowPoint)
                    {
                        result.Add(new int[] { i, j });
                    }
                }
            }

            return result;
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                int[] currentHeatmapRow = inputLine
                    .ToCharArray()
                    .Select(x => int.Parse(x.ToString()))
                    .ToArray();

                InputHeatmap.Add(currentHeatmapRow);

                inputLine = Console.ReadLine();
            }
        }
    }
}
