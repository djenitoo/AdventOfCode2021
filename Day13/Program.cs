using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Program
    {
        public static List<Point> InputPoints = new List<Point>();

        public static List<string> Folds = new List<string>();

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = ");
            TaskTwo();
        }

        private static long TaskOne()
        {
            long result = 0;
            int maxX = InputPoints.Select(p => p.X).Max(x => x);
            int maxY = InputPoints.Select(p => p.Y).Max(x => x);

            List<int[]> matrix = new List<int[]>();

            for (int i = 0; i < maxY + 1; i++)
            {
                matrix.Add(new int[maxX + 1]);
            }

            MarkPoints(matrix, InputPoints);

            foreach (string foldCmd in Folds.Take(1))
            {
                char direction = foldCmd[0];
                int index = int.Parse(foldCmd.Substring(2).Trim());
                matrix = ProcessFold(matrix, direction, index);
            }

            result = GetCountOfMarkedPointsInMatrix(matrix);

            return result;
        }

        private static void TaskTwo()
        {
            int maxX = InputPoints.Select(p => p.X).Max(x => x);
            int maxY = InputPoints.Select(p => p.Y).Max(x => x);

            List<int[]> matrix = new List<int[]>();

            for (int i = 0; i < maxY + 1; i++)
            {
                matrix.Add(new int[maxX + 1]);
            }

            MarkPoints(matrix, InputPoints);

            foreach (string foldCmd in Folds)
            {
                char direction = foldCmd[0];
                int index = int.Parse(foldCmd.Substring(2).Trim());
                matrix = ProcessFold(matrix, direction, index);
            }


            // task answer is visualised with the following:
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write(" " + matrix[i][j] + " ");
                    }

                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }

        private static List<int[]> ProcessFold(List<int[]> matrix, char direction, int index)
        {
            List<int[]> foldedMatrix = new List<int[]>();
            int newMatrixXLen = direction == 'x' ? index : matrix[0].Length;
            int newMatrixYLen = direction == 'y' ? index : matrix.Count;

            for (int i = 0; i < newMatrixYLen; i++)
            {
                foldedMatrix.Add(new int[newMatrixXLen]);

                // copy existing points in the fold
                for (int j = 0; j < newMatrixXLen; j++)
                {
                    foldedMatrix[i][j] = matrix[i][j];
                }
            }

            // get new points
            List<Point> foldedPoints = GetFoldedPoints(matrix, direction, index);

            MarkPoints(foldedMatrix, foldedPoints);

            return foldedMatrix;
        }

        private static List<Point> GetFoldedPoints(List<int[]> matrix, char direction, int index)
        {
            List<Point> result = new List<Point>();
            int startX = direction == 'x' ? index + 1 : 0;
            int endX = matrix[0].Length;
            int startY = direction == 'y' ? index + 1 : 0;
            int endY = matrix.Count();

            for (int i = startY; i < endY; i++)
            {
                for (int j = startX; j < endX; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        var foldedPoint = new Point()
                        {
                            X = direction == 'x' ? (endX - 1) - j : j,
                            Y = direction == 'y' ? (endY - 1) - i : i
                        };

                        result.Add(foldedPoint);
                    }
                }
            }

            return result;
        }

        private static long GetCountOfMarkedPointsInMatrix(List<int[]> matrix)
        {
            long result = 0;

            for (int i = 0; i < matrix.Count; i++)
            {
                result += matrix[i].Sum(x => x);
            }

            return result;
        }

        private static void MarkPoints(List<int[]> matrix, List<Point> points)
        {
            foreach (Point pt in points)
            {
                matrix[pt.Y][pt.X] = 1;
            }
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                int[] currentPoint = inputLine
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x.Trim()))
                    .ToArray();

                InputPoints.Add(new Point() { X = currentPoint[0], Y = currentPoint[1] });

                inputLine = Console.ReadLine();
            }

            inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                string currentFold = inputLine
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Last();

                Folds.Add(currentFold);

                inputLine = Console.ReadLine();
            }
        }
    }
}
