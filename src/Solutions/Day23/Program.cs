using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day23
{
    class Program
    {
        static void Main()
        {
            var input = Input.ReadRows();

            List<Bot> bots = CreateBots(input);
            var part1Answer = CalculateNumberOfNanobotsInRangeOfStrongest(bots);
            var part2Answer = CalculateDistanceInRangeOfLargestAmount(bots);
            Console.WriteLine($"Number of bots: {part1Answer}");
            Console.WriteLine($"Distance: {part2Answer}");
            Console.ReadLine();
        }

        private static long CalculateDistanceInRangeOfLargestAmount(List<Bot> bots)
        {
            var xMin = bots.Min(b => b.X);
            var xMax = bots.Max(b => b.X);
            var yMin = bots.Min(b => b.Y);
            var yMax = bots.Max(b => b.Y);
            var zMin = bots.Min(b => b.Z);
            var zMax = bots.Max(b => b.Z);

            long dist = 1;
            while (dist < xMax - xMin)
            {
                dist *= 2;
            }

            var maxCount = 0;
            var pos = (long.MinValue, long.MinValue, long.MinValue);
            long distance = 0;
            while (true)
            {
                for (var x = xMin; x < xMax; x += dist)
                {
                    for (var y = yMin; y < yMax; y += dist)
                    {
                        for (var z = zMin; z < zMax; z += dist)
                        {
                            var count = bots.Count(b => b.InRange(x, y, z));
                            if (count > maxCount)
                            {
                                maxCount = count;
                                pos = (x, y, z);
                            }
                            else if (count == maxCount)
                            {
                                if (distance == 0 || Math.Abs(x) + Math.Abs(y) + Math.Abs(z) < distance)
                                {
                                    distance = Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
                                    pos = (x, y, z);
                                }
                            }
                        }
                    }
                }

                if (dist == 1)
                {
                    return distance;
                }

                xMin = pos.Item1 - dist;
                xMax = pos.Item1 + dist;
                yMin = pos.Item2 - dist;
                yMax = pos.Item2 + dist;
                zMin = pos.Item3 - dist;
                zMax = pos.Item3 + dist;
                dist /= 2;
            }
        }

        private static int CalculateNumberOfNanobotsInRangeOfStrongest(List<Bot> bots)
        {
            var strongest = bots.OrderByDescending(b => b.Range).FirstOrDefault();
            var count = 0;
            foreach (var bot in bots)
            {
                if (bot.InRangeOf(strongest))
                    count++;
            }

            return count;
        }

        private static List<Bot> CreateBots(string[] input)
        {
            var bots = new List<Bot>();
            foreach (var line in input)
            {
                var parts = line.Split(new[] { "pos=<", ">", "r=", ", ", "," }, StringSplitOptions.RemoveEmptyEntries);
                bots.Add(new Bot(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]), long.Parse(parts[3])));
            }

            return bots;
        }
    }
}
