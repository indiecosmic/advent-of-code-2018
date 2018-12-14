using System;
using System.IO;

namespace AdventOfCode.Common
{
    public static class Input
    {
        public static string[] ReadRows(bool trim = true)
        {
            return ReadInput(trim).Split("\n", StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ReadInput(bool trim = true)
        {
            const string filename = "input.txt";
            using (var reader = new StreamReader(filename))
            {
                var input = reader.ReadToEnd();
                return trim ? input.Trim() : input;
            }
        }
    }
}
