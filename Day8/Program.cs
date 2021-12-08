using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    class Program
    {
        public static List<List<string>> inputOutputValues = new List<List<string>>();
        public static List<List<string>> inputDefiningValues = new List<List<string>>();

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;

            foreach (List<string> currentOutputValues in inputOutputValues)
            {
                foreach (string outputVal in currentOutputValues)
                {
                    switch (outputVal.Length)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                            result++;
                            break;
                        default:
                            break;
                    }
                }
            }

            return result;
        }

        private static long TaskTwo()
        {
            long result = 0;

            foreach (List<string> currentOutputValues in inputOutputValues)
            {
                string currentVal = "";
                var currentDictionary = ResolveInputDictionary(inputDefiningValues[inputOutputValues.IndexOf(currentOutputValues)]);

                foreach (string outputVal in currentOutputValues)
                {
                    //currentVal += currentDictionary[outputVal];

                    switch (outputVal.Length)
                    {
                        case 2:
                            currentVal += 1;
                            break;
                        case 3:
                            currentVal += 7;
                            break;
                        case 4:
                            currentVal += 4;
                            break;
                        case 7:
                            currentVal += 8;
                            break;
                        default:
                            int currentLen = outputVal.Length;
                            List<string> possibleKeys = currentDictionary.Keys.Where(k => k.Length == currentLen).ToList();
                            string key = possibleKeys.FirstOrDefault(k => outputVal.All(x => k.Contains(x)));

                            currentVal += currentDictionary[key];
                            break;
                    }
                }

                result += int.Parse(currentVal);
            }

            return result;
        }

        //private static long TaskTwo()
        //{
        //    long result = 0;

        //    foreach (List<string> currentOutputValues in inputOutputValues)
        //    {
        //        string currentVal = "";

        //        foreach (string outputVal in currentOutputValues)
        //        {
        //            switch (outputVal.Length)
        //            {
        //                case 2:
        //                    currentVal += 1;
        //                    break;
        //                case 3:
        //                    currentVal += 7;
        //                    break;
        //                case 4:
        //                    currentVal += 4;
        //                    break;
        //                case 7:
        //                    currentVal += 8;
        //                    break;
        //                default:
        //                    var chArr = outputVal.ToCharArray();

        //                    if (chArr.Contains('g') && chArr.Contains('e'))
        //                    {
        //                        //if (chArr.All(ch => "bcdefg".IndexOf(ch) >= 0))
        //                        //{
        //                        //    currentVal += 6;
        //                        //}
        //                        //else if (chArr.All(ch => "abcdeg".IndexOf(ch) >= 0))
        //                        //{
        //                        //    currentVal += 0;
        //                        //}
        //                        currentVal += chArr.Contains('a') ? 0 : 6;
        //                    }
        //                    else if (chArr.Contains('e'))
        //                    {
        //                        //else if (chArr.All(ch => "abcdef".IndexOf(ch) >= 0))
        //                        //{
        //                        //    currentVal += 9;
        //                        //}
        //                        //else if (chArr.All(ch => "bcdef".IndexOf(ch) >= 0))
        //                        //{
        //                        //    currentVal += 5;
        //                        //}

        //                        currentVal += chArr.Contains('a') ? 9 : 5;
        //                    }
        //                    else
        //                    {
        //                        //else if (chArr.All(ch => "acdfg".IndexOf(ch) >= 0))
        //                        //{
        //                        //    currentVal += 2;
        //                        //}
        //                        //else if (chArr.All(ch => "abcdf".IndexOf(ch) >= 0))
        //                        //{
        //                        //    currentVal += 3;
        //                        //}

        //                        currentVal += chArr.Contains('b') ? 3 : 2;
        //                    }

        //                    break;
        //            }
        //        }

        //        result += int.Parse(currentVal);
        //    }

        //    return result;
        //}

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                List<string> currentDefiningValues = inputLine
                    .Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[0]
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToList();

                inputDefiningValues.Add(currentDefiningValues);

                List<string> currentOutputValues = inputLine
                    .Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)[1]
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToList();

                inputOutputValues.Add(currentOutputValues);

                inputLine = Console.ReadLine();
            }
        }

        private static Dictionary<string, int> ResolveInputDictionary(List<string> inputDictionary)
        {
            string input1 = inputDictionary.FirstOrDefault(v => v.Length == 2);
            string input7 = inputDictionary.FirstOrDefault(v => v.Length == 3);
            string input4 = inputDictionary.FirstOrDefault(v => v.Length == 4);
            string input8 = inputDictionary.FirstOrDefault(v => v.Length == 7);

            Dictionary<string, int> result = new Dictionary<string, int>()
            {
                { input1, 1 },
                { input7, 7 },
                { input4, 4 },
                { input8, 8 }
            };

            List<string> len6Inputs = inputDictionary.Where(x => x.Length == 6).ToList();
            List<string> len5Inputs = inputDictionary.Where(x => x.Length == 5).ToList();

            string input6 = len6Inputs.FirstOrDefault(x => input1.Any(c => x.IndexOf(c) < 0));

            result.Add(input6, 6);

            string input9 = len6Inputs.FirstOrDefault(x => x != input6 && input1.All(c => x.Contains(c)) && input4.All(c => x.Contains(c)));

            result.Add(input9, 9);
            result.Add(len6Inputs.Where(x => input6 != x && input9 != x).FirstOrDefault(), 0);

            string input5 = len5Inputs.FirstOrDefault(x => x.All(c => input6.Contains(c)));

            result.Add(input5, 5);

            string input3 = len5Inputs.FirstOrDefault(x => x != input5 && x.All(c => input9.Contains(c)));

            result.Add(input3, 3);

            result.Add(len5Inputs.FirstOrDefault(x => x != input3 && x != input5), 2);

            return result;
        }
    }
}
