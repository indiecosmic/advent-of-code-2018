using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day10
{
    class Program
    {
        static void Main()
        {
            var rows = Input.ReadRows();
            var stars = BuildStarsList(rows);

            var width = stars.Max(s => s.X) - stars.Min(s => s.X);
            var count = 0;
            while (width > 70)
            {
                Update(stars);
                width = stars.Max(s => s.X) - stars.Min(s => s.X);
                count++;
            }

            Draw(stars);
            Console.WriteLine($"Number of seconds: {count}");
            Console.ReadLine();
        }

        private static List<Star> BuildStarsList(string[] strings)
        {
            var list = new List<Star>();
            foreach (var row in strings)
            {
                var parts = row.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                var position = parts[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
                var velocity = parts[3].Split(",", StringSplitOptions.RemoveEmptyEntries);
                list.Add(new Star(int.Parse(position[0]), int.Parse(position[1]), int.Parse(velocity[0]),
                    int.Parse(velocity[1])));
            }

            return list;
        }

        private static void Update(List<Star> stars)
        {
            foreach (var star in stars)
            {
                star.Tick();
            }
        }

        private static void Draw(List<Star> stars)
        {
            for (var y = stars.Min(s => s.Y); y <= stars.Max(m => m.Y); y++)
            {
                for (var x = stars.Min(s => s.X); x <= stars.Max(s => s.X); x++)
                {
                    Console.Write(stars.Any(s => s.X == x && s.Y == y) ? "#" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}
