using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Common;

namespace Day12
{
    class Program
    {
        static void Main()
        {
            var input = Input.ReadRows();
            var initialState = input[0].Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

            var patterns = GetPatterns(input.Skip(1));

            var part1Answer = CalculateGenerations(initialState, patterns, 20);
            Console.WriteLine(part1Answer);

            var generation99 = CalculateGenerations(initialState, patterns, 99);
            var generation100 = CalculateGenerations(initialState, patterns, 100);
            var diff = generation100 - generation99;

            var part2Answer = ((50000000000 - 100) * diff) + generation100;
            Console.WriteLine(part2Answer);

            Console.ReadLine();
        }

        private static int CalculateGenerations(string initialState, Dictionary<string, string> patterns, int count)
        {
            var indexes = Enumerable.Range(0, initialState.Length).ToList();
            var pots = Pad(initialState, indexes);
            for (var i = 0; i < count; i++)
            {
                pots = Generate(pots, patterns);
                pots = Pad(pots, indexes);
            }

            var part1Answer = CalculatePotValues(pots, indexes);
            return part1Answer;
        }


        private static Dictionary<string, string> GetPatterns(IEnumerable<string> input) => input
            .Select(s => s.Split(" => ", StringSplitOptions.RemoveEmptyEntries)).ToDictionary(s => s[0], s => s[1]);

        private static string Generate(string state, Dictionary<string, string> patterns)
        {
            StringBuilder result = new StringBuilder();
            for (var i = 2; i < state.Length - 2; i++)
            {
                var input = state.Substring(i - 2, 5);
                patterns.TryGetValue(input, out var value);
                result.Append(value ?? ".");
            }


            return $"..{result}..";

        }

        private static string Pad(string pots, List<int> indexes)
        {
            if (pots.IndexOf("#", 0, 5, StringComparison.Ordinal) != -1)
            {
                pots = $"....{pots}";
                indexes.Insert(0, indexes.First()-1);
                indexes.Insert(0, indexes.First()-1);
                indexes.Insert(0, indexes.First()-1);
                indexes.Insert(0, indexes.First()-1);
            }
            if (pots.IndexOf("#", pots.Length - 6, 5, StringComparison.Ordinal) != -1)
            {
                pots = $"{pots}....";
                indexes.Add(indexes.Last()+1);
                indexes.Add(indexes.Last()+1);
                indexes.Add(indexes.Last()+1);
                indexes.Add(indexes.Last()+1);
            }
            return pots;
        }

        private static int CalculatePotValues(string pots, List<int> indexes)
        {
            var result = 0;
            for (var i = 0; i < pots.Length; i++)
            {
                if (pots[i] == '#')
                {
                    result += indexes[i];
                }
            }
            return result;
        }
    }
}
