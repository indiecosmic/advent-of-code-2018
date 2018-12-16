using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day16
{
    class Sample
    {
        public int[] Register { get; }
        public int[] Opcode { get; }
        public int[] Expected { get; }

        public int OpCodeNumber => Opcode[0];

        private Sample(int[] register, int[] opcode, int[] expected)
        {
            Register = register;
            Opcode = opcode;
            Expected = expected;
        }

        public static Sample Parse(string before, string opcode, string after)
        {
            var register = before.Split(new[] {"Before: ", "[", "]", ", ", " "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            var expected = after.Split(new[] {"After: ", "[", "]", ", ", " "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            var op = opcode.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            return new Sample(register, op, expected);
        }
    }
}
