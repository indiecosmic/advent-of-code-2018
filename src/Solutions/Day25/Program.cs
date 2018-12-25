using System;
using System.Collections.Generic;
using AdventOfCode.Common;

namespace Day25
{
    class Program
    {
        static void Main()
        {
            var input = Input.ReadRows();
            var points = new List<(int x, int y, int z, int t)>();
            foreach (var line in input)
            {
                var parts = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                var point = (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
                points.Add(point);
            }
            var part1Answer = CalculateNumberOfConstellations(points);
            Console.WriteLine($"Number of constellations: {part1Answer}");
            Console.ReadLine();
        }

        private static int CalculateNumberOfConstellations(List<(int x, int y, int z, int t)> points)
        {
            var constellations = new List<Constellation>();

            while (points.Count > 0)
            {
                var added = 0;
                foreach (var constellation in constellations)
                {
                    var remainingPoints = new List<(int x, int y, int z, int t)>();
                    foreach (var point in points)
                    {
                        if (constellation.CanJoin(point))
                        {
                            constellation.Join(point);
                            added++;
                        }
                        else
                        {
                            remainingPoints.Add(point);
                        }
                    }

                    points = remainingPoints;
                }

                if (added == 0)
                {
                    var point = points[0];
                    constellations.Add(new Constellation(point));
                    points.Remove(point);
                }
            }

            var part1Answer = constellations.Count;
            return part1Answer;
        }
    }
}
