using System;
using System.Collections.Generic;
using System.IO;

namespace Day01
{
    internal static class Program
    {
        private static readonly List<int> History = new List<int>{0};

        private static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2018: Day 1");
            var input = GetInputRows();
            var part1Solution = FindResultingFrequency(input);

            Console.WriteLine($"Part 1 solution: {part1Solution}");

            var part2Solution = FindFirstRepeatedFrequency(input);

            Console.WriteLine($"Part 2 solution: {part2Solution}");
            Console.ReadLine();
        }

        private static int FindResultingFrequency(IEnumerable<string> input)
        {
            var value = 0;
            foreach (var row in input)
            {
                var operation = row.Substring(0, 1);
                var number = Convert.ToInt32(row.Substring(1, row.Length - 1));
                if (operation == "+")
                    value += number;
                else if (operation == "-")
                {
                    value -= number;
                }
            }
            return value;
        }

        private static int FindFirstRepeatedFrequency(string[] input, int value = 0)
        {
            while (true)
            {
                foreach (var row in input)
                {
                    var operation = row.Substring(0, 1);
                    var number = Convert.ToInt32(row.Substring(1, row.Length - 1));
                    if (operation == "+")
                    {
                        value += number;
                    }
                    else if (operation == "-")
                    {
                        value -= number;
                    }
                    if (!History.Contains(value))
                        History.Add(value);
                    else
                        return value;
                }
            }
        }

        private static string[] GetInputRows()
        {
            const string filename = "input.txt";
            using (var reader = new StreamReader(filename))
            {
                var input = reader.ReadToEnd();
                return input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
