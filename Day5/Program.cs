using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public struct Line
    {
        public Point Start { get; set; }

        public Point End { get; set; }

        public List<Point> GetPointsOfLine()
        {
            var points = new List<Point>();

            if (Start.X != End.X && Start.Y != End.Y)
            {
                return GetDiagonalPoints();
            }

            bool isHorizontal = Start.X == End.X;
            bool forwardOrientation = isHorizontal ? Start.Y <= End.Y : Start.X <= End.X;

            int startX = isHorizontal ? Start.X : forwardOrientation ? Start.X : End.X;
            int endX = startX == Start.X ? End.X : Start.X;

            int startY = isHorizontal ? forwardOrientation ? Start.Y : End.Y : Start.Y;
            int endY = startY == Start.Y ? End.Y : Start.Y;

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    points.Add(new Point { X = x, Y = y });
                }
            }

            return points;
        }

        public List<Point> GetDiagonalPoints()
        {
            var points = new List<Point>();

            Point startPoint = Start.X > End.X ? End : Start;
            Point endPoint = Start.X > End.X ? Start : End;
            bool directionForward = startPoint.Y < endPoint.Y;
            int y = startPoint.Y;

            for (int x = startPoint.X; x <= endPoint.X; x++)
            {
                points.Add(new Point { X = x, Y = y });

                if (directionForward)
                {
                    y++;
                }
                else
                {
                    y--;
                }
            }

            return points;
        }
    }

    class Program
    {
        public static List<Line> InputLines = new List<Line>();

        static void Main(string[] args)
        {
            ReadInput();

            Console.WriteLine("Final result TaskOne = " + TaskOne());
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;

            var maxX = GetMaxX() + 1;
            var maxY = GetMaxY() + 1;

            int[,] matrix = new int[maxY, maxX];

            MarkLinesOnMatrix(matrix, InputLines.Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y).ToList());

            int overlappingCount = GetOverlappingPointsCount(matrix);
            result = overlappingCount;

            return result;
        }

        private static long TaskTwo()
        {
            long result = 0;

            var maxX = GetMaxX() + 1;
            var maxY = GetMaxY() + 1;

            int[,] matrix = new int[maxY, maxX];

            MarkLinesOnMatrix(matrix, InputLines);

            int overlappingCount = GetOverlappingPointsCount(matrix);
            result = overlappingCount;

            return result;
        }

        private static int GetOverlappingPointsCount(int[,] matrix)
        {
            int overlappingCount = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] >= 2)
                    {
                        overlappingCount++;
                    }
                }
            }

            return overlappingCount;
        }

        private static void MarkLinesOnMatrix(int[,] matrix, List<Line> lines)
        {
            foreach (var line in lines)
            {
                var pointsForLine = line.GetPointsOfLine();

                foreach (var linePoint in pointsForLine)
                {
                    matrix[linePoint.Y, linePoint.X] += 1;
                }
            }
        }

        private static void ReadInput()
        {
            string line = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(line))
            {
                int[] currentPoints = line.Split(new char[] { ' ', ',', '-', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToArray();
                var currentLine = new Line()
                {
                    Start = new Point { X = currentPoints[0], Y = currentPoints[1] },
                    End = new Point { X = currentPoints[2], Y = currentPoints[3] }
                };

                InputLines.Add(currentLine);
                line = Console.ReadLine();
            }
        }

        private static int GetMaxX()
        {
            int maxX = 0;

            foreach (var line in InputLines)
            {
                if (line.Start.X > maxX)
                {
                    maxX = line.Start.X;
                }
                if (line.End.X > maxX)
                {
                    maxX = line.End.X;
                }
            }

            return maxX;
        }

        private static int GetMaxY()
        {
            int maxY = 0;
            foreach (var line in InputLines)
            {
                if (line.Start.Y > maxY)
                {
                    maxY = line.Start.Y;
                }
                if (line.End.Y > maxY)
                {
                    maxY = line.End.Y;
                }
            }

            return maxY;
        }
    }
}
