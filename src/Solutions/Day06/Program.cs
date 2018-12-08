using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCode.Common;

namespace Day06
{
    class Program
    {
        static void Main()
        {
            //var input = new[]
            //{
            //    "1, 1",
            //    "1, 6",
            //    "8, 3",
            //    "3, 4",
            //    "5, 5",
            //    "8, 9"
            //};
            var input = Input.ReadRows();
            var coordinates = ParseCoordinates(input);
            var system = CreateCoordinateSystem(coordinates);

            var part1Solution = CalculateLargestArea(coordinates, system);
            Console.WriteLine($"Largest area is {part1Solution}");

            int part2Solution = CalculateSizeOfRegionWithTotalDistance(coordinates, system);
            Console.WriteLine($"Size of region is {part2Solution}");

            Console.ReadLine();
        }

        private static int CalculateSizeOfRegionWithTotalDistance(List<Coordinate> coordinates, int[,] system)
        {
            var size = (int)Math.Sqrt(system.Length);

            for (var row = 0; row < size; row++)
            {
                for (var col = 0; col < size; col++)
                {
                    var p = new Point(col, row);
                    var sumOfDistances = coordinates.Sum(c => c.Distance(p));
                    system[col, row] = sumOfDistances;
                }
            }

            var regionSize = 0;
            foreach (var i in system)
            {
                if (i < 10000) regionSize++;
            }
            return regionSize;
        }

        private static int[,] CreateCoordinateSystem(List<Coordinate> coordinates)
        {
            var width = coordinates.Max(c => c.Location.X);
            var height = coordinates.Max(c => c.Location.Y);
            var size = Math.Max(width, height) + 1;
            var system = new int[size, size];
            return system;
        }

        private static int CalculateLargestArea(List<Coordinate> coordinates, int[,] system)
        {
            var size = (int)Math.Sqrt(system.Length);
            
            for (var row = 0; row < size; row++)
            {
                for (var col = 0; col < size; col++)
                {
                    var p = new Point(col, row);
                    var c = FindClosest(p, coordinates);
                    if (c == null)
                    {
                        system[col, row] = 0;
                    }
                    else if (c.SameAs(p))
                    {
                        system[col, row] = c.Id;
                    }
                    else
                    {
                        system[col, row] = c.Id;
                    }
                }
            }

            var areas = new Dictionary<int, int>();
            foreach (var i in system)
            {
                if (i == 0) continue;
                if (!areas.ContainsKey(i)) areas.Add(i, 0);
                areas[i]++;
            }

            var infiniteCoordinates = FindInfiniteCoordinates(system, size);
            foreach (var coord in infiniteCoordinates)
                areas.Remove(coord);

            return areas.Max(a => a.Value);
        }

        private static List<int> FindInfiniteCoordinates(int[,] system, int size)
        {
            var result = new List<int>();
            for (var x = 0; x < size; x++)
            {
                if (system[x, 0] == 0) continue;
                if (result.Contains(system[x, 0])) continue;
                result.Add(system[x, 0]);
            }
            for (var x = 0; x < size; x++)
            {
                if (system[x, size-1] == 0) continue;
                if (result.Contains(system[x, size-1])) continue;
                result.Add(system[x, size-1]);
            }
            for (var y = 0; y < size; y++)
            {
                if (system[0, y] == 0) continue;
                if (result.Contains(system[0, y])) continue;
                result.Add(system[0, y]);
            }
            for (var y = 0; y < size; y++)
            {
                if (system[size-1, y] == 0) continue;
                if (result.Contains(system[size-1, y])) continue;
                result.Add(system[size-1, y]);
            }

            return result;
        }

        private static Coordinate FindClosest(Point p, List<Coordinate> coordinates)
        {
            var minValue = coordinates.Min(c => c.Distance(p));
            var matches = coordinates.Where(c => c.Distance(p) == minValue).ToList();
            return matches.Count > 1 ? null : matches.FirstOrDefault();
        }

        private static List<Coordinate> ParseCoordinates(string[] rows)
        {
            var result = new List<Coordinate>();
            var id = 1;
            for (var i = 0; i < rows.Length; i++)
            {
                var parts = rows[i].Split(",", StringSplitOptions.RemoveEmptyEntries);
                var location = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
                result.Add(new Coordinate(id, location));
                id++;
            }
            return result;
        }

        private class Coordinate
        {
            public int Id { get; }
            public Point Location { get; }

            public Coordinate(int id, Point location)
            {
                Id = id;
                Location = location;
            }

            public int Distance(Point point)
            {
                return Math.Abs(Location.X - point.X) + Math.Abs(Location.Y - point.Y);
            }

            public bool SameAs(Point point)
            {
                return Location.X == point.X && Location.Y == point.Y;
            }
        }
    }
}
