using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Program
    {
        public static List<int> InitialCrabs = new List<int>();

        static void Main(string[] args)
        {
            ReadInput();

            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());

        }
        private static long TaskOne()
        {
            long result = long.MaxValue;

            int minPosition = InitialCrabs.Min();
            int maxPosition = InitialCrabs.Max();

            for (int position = minPosition; position < maxPosition; position++)
            {
                long currentResult = 0;

                for (int j = 0; j < InitialCrabs.Count; j++)
                {
                    if (position > InitialCrabs[j])
                    {
                        currentResult += position - InitialCrabs[j];
                    }
                    else
                    {
                        currentResult += InitialCrabs[j] - position;
                    }

                    if (currentResult >= result)
                    {
                        break;
                    }
                }

                if (result > currentResult)
                {
                    result = currentResult;
                }
            }

            return result;
        }

        private static long TaskTwo()
        {
            long result = long.MaxValue;

            int minPosition = InitialCrabs.Min();
            int maxPosition = InitialCrabs.Max();

            for (int position = minPosition; position < maxPosition; position++)
            {
                long currentResult = 0;

                for (int j = 0; j < InitialCrabs.Count; j++)
                {
                    if (position > InitialCrabs[j])
                    {
                        currentResult += GetNotConstantFuelRate(position - InitialCrabs[j]);
                    }
                    else
                    {
                        currentResult += GetNotConstantFuelRate(InitialCrabs[j] - position);
                    }

                    if (currentResult >= result)
                    {
                        break;
                    }
                }

                if (result > currentResult)
                {
                    result = currentResult;
                }
            }

            return result;

        }

        private static long GetNotConstantFuelRate(int positionLength)
        {
            long result = 0;

            for (int i = 1; i <= positionLength; i++)
            {
                result += i;
            }

            return result;
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();
            InitialCrabs = inputLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToList();
        }
    }
}
