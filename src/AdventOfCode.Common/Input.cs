using System;
using System.IO;

namespace AdventOfCode.Common
{
    public static class Input
    {
        public static string[] ReadRows()
        {
            return ReadInput().Split("\n", StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ReadInput()
        {
            const string filename = "input.txt";
            using (var reader = new StreamReader(filename))
            {
                var input = reader.ReadToEnd();
                return input.Trim();
            }
        }
    }
}
