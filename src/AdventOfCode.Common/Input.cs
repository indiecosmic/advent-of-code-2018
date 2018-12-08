using System;
using System.IO;

namespace AdventOfCode.Common
{
    public static class Input
    {
        public static string[] ReadRows()
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
