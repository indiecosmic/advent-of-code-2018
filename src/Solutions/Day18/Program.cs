using System;
using System.Collections.Generic;
using AdventOfCode.Common;

namespace Day18
{
    class Program
    {
        static void Main()
        {
            var lines = Input.ReadRows();
            var area = Area.Create(lines);
            var part1Answer = CalculatePart1Answer(area);
            Console.WriteLine($"Resource value after 10 minutes: {part1Answer}");

            var part2Answer = CalculatePart2Answer(area);
            Console.WriteLine($"Resource value after 1000000000 minutes: {part2Answer}");

            Console.ReadLine();
        }

        private static int CalculatePart1Answer(Area area)
        {
            area.Write();

            for (var i = 0; i < 10; i++)
            {
                area = area.Transform();
                area.Write();
            }

            var part1Answer = area.WoodedAcres * area.Lumberyards;
            return part1Answer;
        }
        private static int CalculatePart2Answer(Area area)
        {
            for (var i = 0; i < 1000; i++)
            {
                area = area.Transform();
            }
            var repeatingValues = new List<int>();
            var resourceValue = area.ResourceValue;
            while (!repeatingValues.Contains(resourceValue))
            {
                repeatingValues.Add(resourceValue);
                area = area.Transform();
                resourceValue = area.ResourceValue;
            }

            var remain = 1000000000 - 1000;
            var index = remain % repeatingValues.Count;
            return repeatingValues[index];
        }
    }
}
