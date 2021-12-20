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

        private static long TaskTwo()
        {
            long result = 0;
            int maxSteps = 40;
            var possibleChars = PairInsertionRules.Values.Distinct().Select(x => char.Parse(x)).ToList();
            
            foreach (var ch in possibleChars)
            {
                countOfChars.Add(ch, 0);
            }

            for (int i = 0; i < PolymerTemplate.Length; i++)
            {
                countOfChars[PolymerTemplate[i]] += 1;
            }

            List<string> initialTemplatePairs = GetPairsFromTemplate(PolymerTemplate);

            IterateSteps(initialTemplatePairs, maxSteps);

            long maxOccurence = countOfChars.Values.Max();
            long minOccurence = countOfChars.Values.Min();

            result = maxOccurence - minOccurence;

            return result;
        }

        private static void IterateSteps(List<string> initialPairs, int maxSteps)
        {
            List<Dictionary<string, long>> pairsBySteps = new List<Dictionary<string, long>>();

            var step0DictionaryOfPairs = new Dictionary<string, long>();

            foreach (var pr in initialPairs)
            {
                if (!step0DictionaryOfPairs.ContainsKey(pr))
                {
                    step0DictionaryOfPairs.Add(pr, 0);
                }

                step0DictionaryOfPairs[pr] += 1;
            }

            pairsBySteps.Add(step0DictionaryOfPairs);

            for (int i = 1; i <= maxSteps; i++)
            {
                pairsBySteps.Add(GetNextRoundChildPairs(pairsBySteps[i - 1]));
            }
        }

        private static Dictionary<string, long> GetNextRoundChildPairs(Dictionary<string, long> parentPairs)
        {
            Dictionary<string, long> childPairs = new Dictionary<string, long>();
            
            foreach (var kVPair in parentPairs)
            {
                string parentPair = kVPair.Key;

                char resultOfPair = PairInsertionRules[parentPair][0];
                countOfChars[resultOfPair] += kVPair.Value;
                string firstPair = parentPair[0].ToString() + resultOfPair;
                string secondPair = resultOfPair + parentPair[1].ToString();

                if (!childPairs.ContainsKey(firstPair))
                {
                    childPairs.Add(firstPair, 0);
                }

                if (!childPairs.ContainsKey(secondPair))
                {
                    childPairs.Add(secondPair, 0);
                }

                childPairs[firstPair] += kVPair.Value;
                childPairs[secondPair] += kVPair.Value;
            }

            return childPairs;
        }

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
