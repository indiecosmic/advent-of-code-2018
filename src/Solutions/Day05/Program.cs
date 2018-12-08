using System;
using System.Globalization;
using System.Linq;
using AdventOfCode.Common;

namespace Day05
{
    class Program
    {
        static void Main()
        {
            var input = Input.ReadInput();
            var polymer = input;

            var part1Answer = CalculateNumberOfUnits(polymer);
            Console.WriteLine($"Number of units: {part1Answer}");

            var part2Answer = CalculateShortestNumberOfUnits(polymer);
            Console.WriteLine($"Shortest number of units: {part2Answer}");

            Console.ReadLine();
        }

        private static int CalculateShortestNumberOfUnits(string polymer)
        {
            var distinctLetters = string.Concat(polymer.ToLower().Distinct().OrderBy(c => c));
            Console.WriteLine($"Distinct letters: {distinctLetters}");
            var minResult = int.MaxValue;
            foreach (var character in distinctLetters)
            {
                var letter = character.ToString();
                var reducedPolymer = polymer.Replace(letter, "", true, CultureInfo.InvariantCulture);
                var unitCount = CalculateNumberOfUnits(reducedPolymer);
                Console.WriteLine($"{character} gives {unitCount}");
                if (unitCount < minResult)
                    minResult = unitCount;
            }

            return minResult;
        }

        static bool TryMakeReaction(string polymer, out string result)
        {
            for (var i = 0; i < polymer.Length -1 ; i++)
            {
                var x = polymer.Substring(i, 1);
                var y = polymer.Substring(i + 1, 1);
                if (x == y) continue;
                if (!x.Equals(y, StringComparison.OrdinalIgnoreCase)) continue;
                result = polymer.Remove(i, 2);
                return true;
            }

            result = polymer;
            return false;
        }

        static int CalculateNumberOfUnits(string polymer)
        {
            while (TryMakeReaction(polymer, out string result))
            {
                polymer = result;
            }
            return polymer.Length;
        }
    }
}
