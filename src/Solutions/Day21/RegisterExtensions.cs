using System;
using System.Collections.Generic;
using System.Text;

namespace Day21
{
    public static class RegisterExtensions
    {
        public static void WriteLine(this int[] register)
        {
            Console.WriteLine($"[{register[0]}, {register[1]}, {register[2]}, {register[3]}, {register[4]}, {register[5]}]");
        }

        public static void Write(this int[]register)
        {
            Console.Write($"[{register[0]}, {register[1]}, {register[2]}, {register[3]}, {register[4]}, {register[5]}] ");
        }
    }
}
