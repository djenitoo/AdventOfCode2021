using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    class Program
    {
        public static List<int[]> InputOctopuses = new List<int[]>();

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;
            int totalSteps = 100;
            int rowLength = InputOctopuses[0].Length;
            var octopusesMatrix = new List<int[]>();

            for (int i = 0; i < InputOctopuses.Count; i++)
            {
                var currentRow = new int[rowLength];
                InputOctopuses[i].CopyTo(currentRow, 0);
                octopusesMatrix.Add(currentRow);
            }

            for (int i = 1; i <= totalSteps; i++)
            {
                var alreadyFlashedOctopusesMatrix = GetBlankInputOctopuses();
                IncreaseOctopusesMatrixByOne(octopusesMatrix);
                RunOctopusFlashes(octopusesMatrix, alreadyFlashedOctopusesMatrix);
                result += CountFlashedOctopuses(alreadyFlashedOctopusesMatrix);
            }

            return result;
        }

        private static long TaskTwo()
        {
            long result = 0;
            int totalSteps = 100;
            int rowLength = InputOctopuses[0].Length;
            var octopusesMatrix = new List<int[]>();

            for (int i = 0; i < InputOctopuses.Count; i++)
            {
                var currentRow = new int[rowLength];
                InputOctopuses[i].CopyTo(currentRow, 0);
                octopusesMatrix.Add(currentRow);
            }

            int s = 1;
            while(result == 0)
            {
                var alreadyFlashedOctopusesMatrix = GetBlankInputOctopuses();
                IncreaseOctopusesMatrixByOne(octopusesMatrix);
                RunOctopusFlashes(octopusesMatrix, alreadyFlashedOctopusesMatrix);
                bool allFlashed = CountFlashedOctopuses(alreadyFlashedOctopusesMatrix) == InputOctopuses.Count * rowLength;
                if (allFlashed)
                {
                    result = s;
                    break;
                }
                s++;
            }

            return result;
        }


        private static void IncreaseOctopusesMatrixByOne(List<int[]> octopuses)
        {
            for (int i = 0; i < octopuses.Count; i++)
            {
                for (int j = 0; j < octopuses[i].Length; j++)
                {
                    octopuses[i][j] += 1;
                }
            }
        }

        private static void RunOctopusFlashes(List<int[]> octopuses, List<int[]> alreadyFlasedOctopuses)
        {
            for (int i = 0; i < octopuses.Count; i++)
            {
                for (int j = 0; j < octopuses[i].Length; j++)
                {
                    ProcessOctopusFlash(octopuses, alreadyFlasedOctopuses, i, j);
                }
            }
        }

        private static int CountFlashedOctopuses(List<int[]> flashedOctopuses)
        {
            int result = 0;

            for (int i = 0; i < flashedOctopuses.Count; i++)
            {
                result += flashedOctopuses[i].Sum(x => x);
            }

            return result;
        }

        private static void ProcessOctopusFlash(List<int[]> octopuses, List<int[]> alreadyFlashedOctopuses, int row, int col)
        {
            if (octopuses[row][col] > 9 && alreadyFlashedOctopuses[row][col] != 1)
            {
                alreadyFlashedOctopuses[row][col] = 1;
                octopuses[row][col] = 0;
            }
            else if (alreadyFlashedOctopuses[row][col] == 1)
            {
                octopuses[row][col] = 0;
                return;
            }
            else
            {
                return;
            }

            // check for flashes on all adjestend octopuses
            // top, down, left, right, top-left, top-right, down-left, down-right

            // top
            if (row - 1 >= 0)
            {
                octopuses[row - 1][col] += 1;
                int topVal = octopuses[row - 1][col];

                //if (topVal > 9)
                //{
                ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row - 1, col);
                //}
            }

            // bottom
            if (row + 1 < octopuses.Count)
            {
                octopuses[row + 1][col] += 1;
                int bottomVal = octopuses[row + 1][col];

                //if (bottomVal > 9)
                //{
                    ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row + 1, col);
                //}
            }

            // left
            if (col - 1 >= 0)
            {
                octopuses[row][col - 1] += 1;
                int leftVal = octopuses[row][col - 1];

                //if (leftVal > 9)
                //{
                    ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row, col - 1);
                //}
            }

            // right
            if (col + 1 < octopuses[0].Length)
            {
                octopuses[row][col + 1] += 1;
                int rightVal = octopuses[row][col + 1];

                //if (rightVal > 9)
                //{
                    ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row, col + 1);
                //}
            }

            // top-left
            if (row - 1 >= 0 && col - 1 >= 0)
            {
                octopuses[row - 1][col - 1] += 1;
                int topLeft = octopuses[row - 1][col - 1];

                //if (topLeft > 9)
                //{
                    ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row - 1, col - 1);
                //}
            }

            // top-right
            if (row - 1 >= 0 && col + 1 < octopuses[0].Length)
            {
                octopuses[row - 1][col + 1] += 1;
                int topRight = octopuses[row - 1][col + 1];

                //if (topRight > 9)
                //{
                    ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row - 1, col + 1);
                //}
            }

            // down-left
            if (row + 1 < octopuses.Count && col - 1 >= 0)
            {
                octopuses[row + 1][col - 1] += 1;
                int downLeft = octopuses[row + 1][col - 1];

                //if (downLeft > 9)
                //{
                    ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row + 1, col - 1);
                //}
            }

            // down-right
            if (row + 1 < octopuses.Count && col + 1 < octopuses[0].Length)
            {
                octopuses[row + 1][col + 1] += 1;
                int downRight = octopuses[row + 1][col + 1];

                //if (downRight > 9)
                //{
                    ProcessOctopusFlash(octopuses, alreadyFlashedOctopuses, row + 1, col + 1);
                //}
            }
        }

        private static List<int[]> GetBlankInputOctopuses()
        {
            var result = new List<int[]>();
            int rowLength = InputOctopuses[0].Length;

            for (int i = 0; i < InputOctopuses.Count; i++)
            {
                result.Add(new int[rowLength]);
            }

            return result;
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                int[] currentOctopusesRow = inputLine
                    .ToCharArray()
                    .Select(x => int.Parse(x.ToString()))
                    .ToArray();

                InputOctopuses.Add(currentOctopusesRow);

                inputLine = Console.ReadLine();
            }
        }
    }
}
