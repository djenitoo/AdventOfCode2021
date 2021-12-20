using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {
        public static string PolymerTemplate = string.Empty;
        public static Dictionary<string, string> PairInsertionRules = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;
            int maxSteps = 10;

            string growingPolymerTemplate = new string(PolymerTemplate.ToCharArray());

            for (int i = 0; i < maxSteps; i++)
            {
                string currentStepPolymerTemplate = growingPolymerTemplate.Substring(0);

                for (int j = 0; j < currentStepPolymerTemplate.Length - 1; j++)
                {
                    growingPolymerTemplate = growingPolymerTemplate.Insert((j * 2) + 1, PairInsertionRules[currentStepPolymerTemplate.Substring(j, 2)]);
                }
            }

            Dictionary<char, long> charsCountInPolymerTemplate = new Dictionary<char, long>();
            var possibleChars = PairInsertionRules.Values.Distinct().Select(x => char.Parse(x)).ToList();

            foreach (char possibleChar in possibleChars)
            {
                long currentCharCount = growingPolymerTemplate.Where(x => x == possibleChar).Count();

                charsCountInPolymerTemplate.Add(possibleChar, currentCharCount);
            }

            long maxOccurence = charsCountInPolymerTemplate.Values.Max();
            long minOccurence = charsCountInPolymerTemplate.Values.Min();

            result = maxOccurence - minOccurence;

            return result;
        }

        private static Dictionary<char, long> countOfChars = new Dictionary<char, long>();
        private static Dictionary<string, long> countOfPairs = new Dictionary<string, long>();

        private static long TaskTwo()
        {
            long result = 0;
            int maxSteps = 40;
            var possibleChars = PairInsertionRules.Values.Distinct().Select(x => char.Parse(x)).ToList();
            var possiblePairs = PairInsertionRules.Keys.Distinct().ToList();

            foreach (var ch in possibleChars)
            {
                countOfChars.Add(ch, 0);
            }

            foreach (var ch in possiblePairs)
            {
                countOfPairs.Add(ch, 0);
            }

            for (int i = 0; i < PolymerTemplate.Length; i++)
            {
                countOfChars[PolymerTemplate[i]] += 1;
            }

            List<string> initialTemplatePairs = GetPairsFromTemplate(PolymerTemplate);


            //IteratePairs(initialTemplatePairs, 1);
            IterateSteps(initialTemplatePairs);

            long maxOccurence = countOfChars.Values.Max();
            long minOccurence = countOfChars.Values.Min();

            result = maxOccurence - minOccurence;

            return result;
        }

        private static  List<List<string>> pairsBySteps = new List<List<string>>();

        private static void IterateSteps(List<string> initialPairs)
        {
            pairsBySteps.Add(new List<string>(initialPairs));

            for (int i = 1; i < 40; i++)
            {
                pairsBySteps.Add(GetNextRoundChildPairs(pairsBySteps[i - 1]));
            }
        } 

        private static List<string> GetNextRoundChildPairs(List<string> parentPairs)
        {
            List<string> childPairs = new List<string>();

            foreach (var parentPair in parentPairs)
            {
                char resultOfPair = PairInsertionRules[parentPair][0];

                childPairs.Add(parentPair[0].ToString() + resultOfPair);
                childPairs.Add(resultOfPair + parentPair[1].ToString());
            }

            return childPairs;
        }

        //private static void IteratePairs(List<string> pairs, int steps)
        //{
        //    //if (steps > 40)
        //    //{
        //    //    return;
        //    //}
        //    Dictionary<string, long> innerCountOfPairs = new Dictionary<string, long>();
        //    var possiblePairs = PairInsertionRules.Keys.Distinct().ToList();

        //    //foreach (var ch in possiblePairs)
        //    //{
        //    //    countOfPairs.Add(ch, 0);
        //    //}

        //    foreach (string pair in pairs)
        //    {
        //        if (countOfPairs.ContainsKey(pair))
        //        {

        //        }
        //        else
        //        {

        //        }
        //        for (int i = 0; i < 40; i++)
        //        {
        //            char resultOfPair = PairInsertionRules[pair][0];
        //            //countOfChars[resultOfPair] += 1;

        //            countOfPairs[pair] += 1;

        //            var newSubPairs = new List<string>();
        //            newSubPairs.Add(pair[0].ToString() + resultOfPair);
        //            newSubPairs.Add(resultOfPair + pair[1].ToString());

        //            IteratePairs(newSubPairs, steps + 1);
        //        }
        //    }
        //}

        private static List<string> GetPairsFromTemplate(string template)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < template.Length - 1; i++)
            {
                result.Add(template.Substring(i, 2));
            }

            return result;
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();
            PolymerTemplate = inputLine.Trim();
            inputLine = Console.ReadLine();
            inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                string[] currentInsertionRule = inputLine
                    .Split(new char[] { ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();

                PairInsertionRules.Add(currentInsertionRule[0], currentInsertionRule[1]);

                inputLine = Console.ReadLine();
            }
        }
    }
}
