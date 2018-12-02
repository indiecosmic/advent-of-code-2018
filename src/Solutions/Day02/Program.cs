using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInputRows();

            var part1Solution = CalculateChecksum(input);
            Console.WriteLine($"Checksum: {part1Solution}");


            var part2Solution = FindCommonLetters(input);
            Console.WriteLine($"Common letters: {part2Solution}");

            Console.ReadLine();
        }

        private static int CalculateChecksum(IEnumerable<string> rows)
        {
            var multiples = new Dictionary<int, int>();

            foreach (var row in rows)
            {
                var distinctLetters = row.Distinct();
                var occurences = new List<int>();
                foreach (var letter in distinctLetters)
                {
                    occurences.Add(GetOccurences(letter, row));
                }
                var distinctOccurences = occurences.Distinct().Where(n => n > 1);
                foreach (var o in distinctOccurences)
                {
                    if (!multiples.ContainsKey(o)) multiples[o] = 0;
                    multiples[o]++;
                }
            }

            var result = 1;
            foreach (var val in multiples.Values)
            {
                result *= val;
            }

            return result;
        }

        private static int GetOccurences(char c, string word)
        {
            var count = 0;
            var length = word.Length;
            for (var n = length - 1; n >= 0; n--)
            {
                if (word[n] == c)
                    count++;
            }
            return count;
        }

        private static string FindCommonLetters(string[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                for (var j = 0; j < words.Length; j++)
                {
                    if (i == j) continue;
                    var distance = LevenshteinDistance(words[i], words[j]);
                    if (distance == 1)
                    {
                        for (var k = 0; k < words[i].Length; k++)
                        {
                            if (words[i][k] != words[j][k])
                                return words[i].Remove(k, 1);
                        }
                    }
                }
            }

            return string.Empty;
        }

        private static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        private static string[] GetInputRows()
        {
            //return new[] {"abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab"};

            const string filename = "input.txt";
            using (var reader = new StreamReader(filename))
            {
                var input = reader.ReadToEnd();
                return input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
