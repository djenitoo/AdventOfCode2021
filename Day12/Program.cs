using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    //public struct Line
    //{
    //    public string Start { get; set; }
    //    public string End { get; set; }
    //}

    public struct Point
    {
        public string Name { get; set; }

        public List<string> Siblings { get; set; }
    }

    class Program
    {
        //public static List<Line> InputLines = new List<Line>();
        public static List<Point> InputPoints = new List<Point>();

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine("Final result TaskOne = " + TaskOne());

            // hella slow - takes ~70min to find final answer, rework to be fast after finding pattern in the data
            Console.WriteLine("Final result TaskTwo = " + TaskTwo());
        }

        private static long TaskOne()
        {
            long result = 0;
            List<List<Point>> possiblePaths = new List<List<Point>>();

            IteratePaths(possiblePaths, new List<Point>(), InputPoints.FirstOrDefault(p => p.Name == "start"));

            foreach (var path in possiblePaths)
            {
                Console.WriteLine(string.Join(",", path.Select(p => p.Name)));
            }

            Console.WriteLine("Total count = " + possiblePaths.Count);

            //foreach (var path in possiblePaths)
            //{
            //    var currentPath = path;
            //    bool visitsSmallCaveAtMostOnce = currentPath.Where(p => IsSmallCaveAndNotEnd(p.Name)).Count() == 1;

            //    if (visitsSmallCaveAtMostOnce)
            //    {
            //        result += 1;
            //    }
            //}

            result = possiblePaths.Count;

            return result;
        }

        private static long TaskTwo()
        {
            long result = 0;
            List<List<Point>> possiblePaths = new List<List<Point>>();

            IteratePaths(possiblePaths, new List<Point>(), InputPoints.FirstOrDefault(p => p.Name == "start"), true);

            foreach (var path in possiblePaths)
            {
                Console.WriteLine(string.Join(",", path.Select(p => p.Name)));
            }

            Console.WriteLine("Total count = " + possiblePaths.Count);

            result = possiblePaths.Count;

            return result;
        }

        private static bool IsSmallCaveAndNotEnd(string name)
        {
            return name != "end" && name != "start" && name.All(c => char.IsLower(c));
        }

        private static void IteratePaths(List<List<Point>> paths, List<Point> currentPath, Point currentPoint, bool canVisitSmallCaveTwiceOnlyOnce = false)
        {
            // add current point
            var newCurrentPath = new List<Point>();
            newCurrentPath.AddRange(currentPath);
            newCurrentPath.Add(currentPoint);

            if (canVisitSmallCaveTwiceOnlyOnce && IsSmallCaveAndNotEnd(currentPoint.Name))
            {
                var twiceSmallCaveAlreadyUsed = newCurrentPath.Where(p => p.Name == currentPoint.Name).Count() > 1;

                if (twiceSmallCaveAlreadyUsed)
                {
                    canVisitSmallCaveTwiceOnlyOnce = false;
                }
            }

            // get next possible points
            var nextPoints = GetNextPossibleStep(newCurrentPath, currentPoint, canVisitSmallCaveTwiceOnlyOnce);

            // if no next points, check if current is "end" point & if you should add it to paths
            if (nextPoints.Count == 0)
            {
                if (currentPoint.Name == "end")
                {
                    string joinedCurrentPath = string.Join(",", newCurrentPath.Select(p => p.Name));
                    bool currentPathExistInPaths = paths.FirstOrDefault(p => string.Join(",", p.Select(x => x.Name)) == joinedCurrentPath) != null;

                    if (!currentPathExistInPaths)
                    {
                        paths.Add(newCurrentPath);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

            }

            foreach (var nextPt in nextPoints.OrderBy(x => x.Name))
            {
                IteratePaths(paths, newCurrentPath, nextPt, canVisitSmallCaveTwiceOnlyOnce);
            }
        }

        private static List<Point> GetNextPossibleStep(List<Point> currentPath, Point currentPoint, bool canVisitSmallCaveTwice = false)
        {
            var result = new List<Point>();
            //canVisitSmallCaveTwice = canVisitSmallCaveTwice ? true : false;
            if (currentPoint.Name == "end")
            {
                return result;
            }

            foreach (var ptName in currentPoint.Siblings)
            {
                if (ptName == "start")
                {
                    continue;
                }
                if (ptName.All(c => char.IsLower(c)) && canVisitSmallCaveTwice && !currentPath.All(p => p.Name != ptName))
                {
                    result.Add(InputPoints.FirstOrDefault(p => p.Name == ptName));
                }
                else if (ptName.All(c => char.IsLower(c)) && currentPath.All(p => p.Name != ptName))
                {
                    result.Add(InputPoints.FirstOrDefault(p => p.Name == ptName));
                }
                else if (ptName.All(c => char.IsUpper(c)))
                {
                    result.Add(InputPoints.FirstOrDefault(p => p.Name == ptName));
                }
            }

            return result;
        }

        //private static List<Line> GetAllPossibleLineDirections()
        //{
        //    List<Line> lines = new List<Line>();

        //    for (int i = 0; i < InputLines.Count; i++)
        //    {
        //        Line currentLine = InputLines[i];
        //        lines.Add(new Line() { Start = currentLine.Start, End = currentLine.End });

        //        if (currentLine.Start != "start" && currentLine.End != "start" && currentLine.End != "end" && currentLine.Start != "start")
        //        {
        //            if (currentLine.Start.All(c => char.IsLower(c)) && currentLine.End.All(c => char.IsLower(c)))
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                lines.Add(new Line { Start = currentLine.End, End = currentLine.Start });
        //            }
        //        }
        //    }

        //    return lines;
        //}

        //private static void ReadInput()
        //{
        //    string inputLine = Console.ReadLine();

        //    while (!string.IsNullOrWhiteSpace(inputLine))
        //    {
        //        string[] currentPathLine = inputLine
        //            .Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

        //        InputLines.Add(new Line() { Start = currentPathLine[0].Trim(), End = currentPathLine[1].Trim() });

        //        inputLine = Console.ReadLine();
        //    }
        //}

        private static void ReadInput()
        {
            string inputLine = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(inputLine))
            {
                string[] currentPathLine = inputLine
                    .Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                var startPoint = new Point() { Name = currentPathLine[0] };
                var endPoint = new Point() { Name = currentPathLine[1] };

                if (InputPoints.Any(p => p.Name == startPoint.Name))
                {
                    var existingPoint = InputPoints.FirstOrDefault(p => p.Name == startPoint.Name);
                    if (!existingPoint.Siblings.Contains(endPoint.Name))
                    {
                        existingPoint.Siblings.Add(endPoint.Name);
                    }
                }
                else
                {
                    startPoint.Siblings = endPoint.Name == "start" ? new List<string>() : new List<string>() { endPoint.Name };
                    InputPoints.Add(startPoint);
                }

                if (InputPoints.Any(p => p.Name == endPoint.Name))
                {
                    var existingPoint = InputPoints.FirstOrDefault(p => p.Name == endPoint.Name);
                    if (!existingPoint.Siblings.Contains(startPoint.Name))
                    {
                        existingPoint.Siblings.Add(startPoint.Name);
                    }
                }
                else
                {
                    endPoint.Siblings = startPoint.Name == "start" ? new List<string>() : new List<string>() { startPoint.Name };
                    InputPoints.Add(endPoint);
                }

                inputLine = Console.ReadLine();
            }
        }
    }
}
