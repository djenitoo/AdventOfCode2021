using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        public static List<int> InitialLanternfish = new List<int>();
        public static ulong PublicTaskTwoResult = 0;

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;
            List<int> laternFishes = new List<int>();
            laternFishes.AddRange(InitialLanternfish);
            int daysLimit = 80;

            for (int f = 0; f < laternFishes.Count; f++)
            {
                var currentResult = new List<int>() { laternFishes[f] };

                for (int i = 0; i < daysLimit; i++)
                {
                    currentResult = GetFishes(currentResult);
                }

                result += currentResult.Count;
            }

            return result;
        }

        private static ulong TaskTwo()
        {
            ulong result = 0;
            List<int> laternFishes = new List<int>();
            laternFishes.AddRange(InitialLanternfish);
            int daysLimit = 256;

            Dictionary<int, ulong> cachedResults = new Dictionary<int, ulong>();

            for (int f = 0; f < laternFishes.Count; f++)
            {
                result = 1;
                //PublicTaskTwoResult++;
                if (!cachedResults.ContainsKey(laternFishes[f]))
                {
                    result = GetRecursiveFishes(laternFishes[f], daysLimit, result);
                    cachedResults.Add(laternFishes[f], result);
                }

                PublicTaskTwoResult += cachedResults[laternFishes[f]];
            }

            return PublicTaskTwoResult;
        }

        private static ulong GetRecursiveFishes(int number, int days, ulong result)
        {
            //PublicTaskTwoResult++;

            int startInterval = (days - number);
            int spawnsForThisParent = startInterval / 7 + (startInterval % 7 > 0 ? 1 : 0);

            if (days < 0)
            {
                return result;
            }

            //if (startInterval == 0)
            //{
            //    PublicTaskTwoResult++;
            //}

            for (int i = 0; i < spawnsForThisParent; i++)
            {
                result++;
                result = GetRecursiveFishes(9, startInterval - (i * 7), result);
            }

            return result;
        }

        //private static void GetRecursiveFishes(int number, int days)
        //{
        //    //PublicTaskTwoResult++;

        //    int startInterval = (days - number);
        //    int spawnsForThisParent = startInterval / 7 + (startInterval % 7 > 0 ? 1 : 0);

        //    if (days < 0)
        //    {
        //        return;
        //    }

        //    //if (startInterval == 0)
        //    //{
        //    //    PublicTaskTwoResult++;
        //    //}

        //    for (int i = 0; i < spawnsForThisParent; i++)
        //    {
        //        PublicTaskTwoResult++;
        //        GetRecursiveFishes(9, startInterval - (i * 7));
        //    }
        //}


        private static List<int> GetFishes(List<int> fishes)
        {
            int zeroFishesCount = fishes.Where(f => f == 0).Count();
            fishes = fishes.Select(x => x == 0 ? 6 : (x - 1)).ToList();

            if (zeroFishesCount > 0)
            {
                for (int i = 0; i < zeroFishesCount; i++)
                {
                    fishes.Add(8);
                }
            }

            return fishes;
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();
            InitialLanternfish = inputLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToList();
        }
    }
}
