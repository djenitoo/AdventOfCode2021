using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
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
            long finalBingoResult = 0;
            List<int> bingoInputNumbers = new List<int>();
            List<int[,]> boards = new List<int[,]>();

            string inputLine = Console.ReadLine();

            bingoInputNumbers = inputLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToList();

            inputLine = Console.ReadLine();
            int[,] currentBoard = null;
            int currentIndex = 0;

            while (inputLine != "END")
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    if (currentBoard != null)
                    {
                        boards.Add(currentBoard);
                    }

                    currentBoard = new int[5, 5];
                    currentIndex = 0;
                }
                else
                {
                    int[] currentLine = inputLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                    for (int i = 0; i < currentLine.Length; i++)
                    {
                        currentBoard[currentIndex, i] = currentLine[i];
                    }

                    currentIndex++;
                }

                inputLine = Console.ReadLine();
            }

            foreach (var bingoNumber in bingoInputNumbers)
            {
                // mark each number
                // check for bingo
                MarkCurrentBingoNumber(boards, bingoNumber);
                var firstBingoBoard = GetFirstBingo(boards);

                if (firstBingoBoard != null)
                {
                    var boardSum = GetSumOfMatrix(firstBingoBoard);
                    finalBingoResult = boardSum * bingoNumber;
                    break;
                }
            }

            return finalBingoResult;
        }

        private static long TaskTwo()
        {
            long finalBingoResult = 0;
            List<int> bingoInputNumbers = new List<int>();
            List<int[,]> boards = new List<int[,]>();

            string inputLine = Console.ReadLine();

            bingoInputNumbers = inputLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToList();

            inputLine = Console.ReadLine();
            int[,] currentBoard = null;
            int currentIndex = 0;

            while (inputLine != "END")
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    if (currentBoard != null)
                    {
                        boards.Add(currentBoard);
                    }

                    currentBoard = new int[5, 5];
                    currentIndex = 0;
                }
                else
                {
                    int[] currentLine = inputLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                    for (int i = 0; i < currentLine.Length; i++)
                    {
                        currentBoard[currentIndex, i] = currentLine[i];
                    }

                    currentIndex++;
                }

                inputLine = Console.ReadLine();
            }

            for (int i = 0; i < bingoInputNumbers.Count; i++)
            {
                var bingoNumber = bingoInputNumbers[i];
                // mark each number
                // check for bingo
                MarkCurrentBingoNumber(boards, bingoNumber);
                var firstBingoBoard = GetFirstBingo(boards);

                while (firstBingoBoard != null)
                {
                    boards.Remove(firstBingoBoard);
                    
                    var boardSum = GetSumOfMatrix(firstBingoBoard);
                    finalBingoResult = boardSum * bingoNumber;

                    firstBingoBoard = GetFirstBingo(boards);
                }
            }

            return finalBingoResult;
        }

        private static void MarkCurrentBingoNumber(List<int[,]> bingoBoards, int number)
        {
            foreach (var board in bingoBoards)
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == number)
                        {
                            board[i, j] = -1;
                        }
                    }
                }
            }
        }

        private static int[,] GetFirstBingo(List<int[,]> bingoBoards)
        {
            int[,] bingoBoard = null;

            foreach (var board in bingoBoards)
            {
                if (bingoBoard != null)
                {
                    break;
                }

                for (int i = 0; i < board.GetLength(1); i++)
                {
                    var currentRow = GetRow(board, i);
                    if (currentRow.Sum(x => x) == -5)
                    {
                        bingoBoard = board;
                        break;
                    }
                }

                if (bingoBoard != null)
                {
                    break;
                }

                for (int i = 0; i < board.GetLength(0); i++)
                {
                    var currentCol = GetColumn(board, i);
                    if (currentCol.Sum(x => x) == -5)
                    {
                        bingoBoard = board;
                        break;
                    }
                }
            }

            return bingoBoard;
        }

        private static int[] GetRow(int[,] matrix, int rowIndex)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowIndex, x])
                .ToArray();
        }

        private static int[] GetColumn(int[,] matrix, int columnIndex)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnIndex])
                .ToArray();
        }

        private static int GetSumOfMatrix(int[,] matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                sum += GetRow(matrix, i).Where(x => x != -1).Sum(x => x);
            }

            return sum;
        }
    }
}
