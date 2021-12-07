using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Final result = " + TaskOne());
            Console.WriteLine("Final result = " + TaskTwo());
        }

        private static long TaskOne()
        {
            List<string> inputs = new List<string>();

            string line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                inputs.Add(line);
                line = Console.ReadLine();
            }

            int lineLength = inputs[0].Length;
            string majorities = string.Empty;
            string minorities = string.Empty;

            for (int i = 0; i < lineLength; i++)
            {
                int majority = 0;
                foreach (var inputLine in inputs)
                {
                    majority += (inputLine[i] == '0' ? -1 : 1);
                }

                if (majority < 0)
                {
                    majorities += "0";
                    minorities += "1";
                }
                else
                {
                    majorities += "1";
                    minorities += "0";
                }
            }

            var gammaRate = Convert.ToInt32(majorities, 2);
            var epsilonRate = Convert.ToInt32(minorities, 2);

            return gammaRate * epsilonRate;
        }

        private static long TaskTwo()
        {
            List<string> inputs = new List<string>();
            long result = 0;

            string line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                inputs.Add(line);
                line = Console.ReadLine();
            }

            List<string> majorities = new List<string>();
            List<string> minorities = new List<string>();
            int lineLength = inputs[0].Length;

            for (int i = 0; i < lineLength; i++)
            {
                int majority = 0;
                foreach (var inputLine in inputs)
                {
                    majority += (inputLine[i] == '0' ? -1 : 1);
                }

                if (majority < 0)
                {
                    majorities = inputs.Where(s => s[0] == '0').ToList();
                    minorities = inputs.Where(s => s[0] == '1').ToList();
                }
                else
                {
                    majorities = inputs.Where(s => s[0] == '1').ToList();
                    minorities = inputs.Where(s => s[0] == '0').ToList();
                }
            }

            long oxygenGeneratorRating = 0;
            long CO2ScrubberRating = 0;
            int itemsLength = majorities[0].Length;

            var currentItemsList = majorities;

            while (currentItemsList.Count > 1)
            {
                for (int i = 1; i < itemsLength; i++)
                {
                    currentItemsList = GetMajoritiesOrMinorities(currentItemsList, i, '1', true);
                    if (currentItemsList.Count == 1)
                    {
                        break;
                    }
                }
            }

            oxygenGeneratorRating = Convert.ToInt32(currentItemsList.FirstOrDefault(), 2);

            currentItemsList = minorities;

            while (currentItemsList.Count > 1)
            {
                for (int i = 1; i < itemsLength; i++)
                {
                    currentItemsList = GetMajoritiesOrMinorities(currentItemsList, i, '0', false);
                    if (currentItemsList.Count == 1)
                    {
                        break;
                    }
                }
            }

            CO2ScrubberRating = Convert.ToInt32(currentItemsList.FirstOrDefault(), 2);

            result = oxygenGeneratorRating * CO2ScrubberRating;

            return result;
        }

        private static List<string> GetMajoritiesOrMinorities(List<string> collection, int index, char overridingValue, bool getMajorities)
        {
            var zeroCount = collection.Where(x => x[index] == '0').Count();
            var oneCount = collection.Where(x => x[index] == '1').Count();

            bool isEqualMajority = zeroCount == oneCount;

            if (isEqualMajority)
            {
                return collection.Where(x => x[index] == overridingValue).ToList();
            }

            char majorityChar = zeroCount > oneCount ? '0' : '1';

            return collection.Where(x => getMajorities ? x[index] == majorityChar : x[index] != majorityChar).ToList();
        }
    }
}
