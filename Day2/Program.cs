using System;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Final result = " + TaskOne());
            Console.WriteLine("Final result = " + TaskTwo());
        }

        private static int TaskOne()
        {
            int forwardCount = 0;
            int downCount = 0;

            string line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                string[] currentCommand = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int currentNumber = int.Parse(currentCommand[1].Trim());

                switch (currentCommand[0].Trim())
                {
                    case "forward":
                        forwardCount += currentNumber;
                        break;
                    case "down":
                        downCount += currentNumber;
                        break;
                    case "up":
                        downCount -= currentNumber;
                        break;
                    default:
                        break;
                }

                line = Console.ReadLine();
            }

            return forwardCount * downCount;
        }

        private static long TaskTwo()
        {
            int forwardCount = 0;
            int aimCount = 0;
            int depthCount = 0;

            string line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                string[] currentCommand = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int currentNumber = int.Parse(currentCommand[1].Trim());

                switch (currentCommand[0].Trim())
                {
                    case "forward":
                        forwardCount += currentNumber;
                        depthCount += aimCount * currentNumber;
                        break;
                    case "down":
                        aimCount += currentNumber;
                        break;
                    case "up":
                        aimCount -= currentNumber;
                        break;
                    default:
                        break;
                }

                line = Console.ReadLine();
            }

            return forwardCount * depthCount;
        }
    }
}
