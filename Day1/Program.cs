using System;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int increaseCount = 0;

            // Task One
            //increaseCount = TaskOne();

            // Task 2:
            increaseCount = TaskTwo();

            // Final
            Console.WriteLine("Final increased count: " + increaseCount);
        }

        private static int TaskOne()
        {
            int increaseCount = 0;
            int prevValue = 0;
            int count = 0;
            string line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                int currentNumber = int.Parse(line.Trim());
                if (currentNumber > prevValue && count != 0)
                {
                    increaseCount++;
                }
                prevValue = currentNumber;
                count++;
                line = Console.ReadLine();
            }

            return increaseCount;
        }

        private static int TaskTwo()
        {
            int increaseCount = 0;
            int prevSum = 0;

            List<int> inputNumbers = new List<int>();
            string line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                int currentNumber = int.Parse(line.Trim());
                inputNumbers.Add(currentNumber);
                line = Console.ReadLine();
            }

            for (int i = 0; i < (inputNumbers.Count - 2); i++)
            {
                int currentSum = inputNumbers[i] + inputNumbers[i + 1] + inputNumbers[i + 2];
                
                if (currentSum > prevSum && i != 0)
                {
                    increaseCount++;
                }

                prevSum = currentSum;
            }

            return increaseCount;
        }
    }
}
