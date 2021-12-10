using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        public static List<string> NavigationSubSystem = new List<string>();
        private static Dictionary<char, char> ChunksDictionary = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        private static Dictionary<char, int> ChunksValues = new Dictionary<char, int>()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        private static Dictionary<char, int> MissingChunksValues = new Dictionary<char, int>()
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;
            List<char> illegalChunks = new List<char>();

            foreach (var chunksLine in NavigationSubSystem)
            {
                List<char[]> currentLineChunks = new List<char[]>();

                for (int i = 0; i < chunksLine.Length; i++)
                {
                    char currentChunkSymbol = chunksLine[i];

                    if (ChunksDictionary.ContainsKey(currentChunkSymbol))
                    {
                        var newChunkPair = new char[2];
                        newChunkPair[0] = currentChunkSymbol;
                        newChunkPair[1] = 'E';
                        currentLineChunks.Add(newChunkPair);
                    }
                    else
                    {
                        var lastNotClosedChunkPair = currentLineChunks.LastOrDefault(ch => ch[1] == 'E');
                        int indexOfNotClosedChunkPair = currentLineChunks.IndexOf(lastNotClosedChunkPair);
                        char validPairChar = ChunksDictionary.FirstOrDefault(x => x.Value == currentChunkSymbol).Key;

                        if (validPairChar != lastNotClosedChunkPair[0])
                        {
                            illegalChunks.Add(currentChunkSymbol);
                            break;
                        }
                        else
                        {
                            currentLineChunks[indexOfNotClosedChunkPair][1] = currentChunkSymbol;
                        }
                    }
                }
            }

            illegalChunks.ForEach(ch => result += ChunksValues[ch]);

            return result;
        }

        private static long TaskTwo()
        {
            long result = 0;
            List<long> legalMissingChunksValues = new List<long>();

            foreach (var chunksLine in NavigationSubSystem)
            {
                List<char[]> currentLineChunks = new List<char[]>();
                bool isValidLine = true;

                for (int i = 0; i < chunksLine.Length; i++)
                {
                    char currentChunkSymbol = chunksLine[i];

                    if (ChunksDictionary.ContainsKey(currentChunkSymbol))
                    {
                        var newChunkPair = new char[2];
                        newChunkPair[0] = currentChunkSymbol;
                        newChunkPair[1] = 'E';
                        currentLineChunks.Add(newChunkPair);
                    }
                    else
                    {
                        var lastNotClosedChunkPair = currentLineChunks.LastOrDefault(ch => ch[1] == 'E');
                        int indexOfNotClosedChunkPair = currentLineChunks.IndexOf(lastNotClosedChunkPair);
                        char validPairChar = ChunksDictionary.FirstOrDefault(x => x.Value == currentChunkSymbol).Key;

                        if (validPairChar != lastNotClosedChunkPair[0])
                        {
                            isValidLine = false;
                            break;
                        }
                        else
                        {
                            currentLineChunks[indexOfNotClosedChunkPair][1] = currentChunkSymbol;
                        }
                    }
                }

                if (!isValidLine)
                {
                    continue;
                }

                List<char> missingLineChunks = currentLineChunks.Where(c => c[1] == 'E').Select(c => ChunksDictionary[c[0]]).Reverse().ToList();

                long currentLineMissingChunksValue = 0;

                foreach (var chunkItem in missingLineChunks)
                {
                    currentLineMissingChunksValue *= 5;
                    currentLineMissingChunksValue += MissingChunksValues[chunkItem];
                }

                legalMissingChunksValues.Add(currentLineMissingChunksValue);
            }

            int middleIndex = legalMissingChunksValues.Count() / 2;

            result = legalMissingChunksValues.OrderByDescending(x => x).ToList()[middleIndex];

            return result;
        }

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                NavigationSubSystem.Add(inputLine);

                inputLine = Console.ReadLine();
            }
        }
    }
}
