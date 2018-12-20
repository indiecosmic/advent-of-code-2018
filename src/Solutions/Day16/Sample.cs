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
        public int OpCodeNumber { get; }

        private Sample(int[] register, int opCodeNumber, int[] opcode, int[] expected)
        {
            Register = register;
            OpCodeNumber = opCodeNumber;
            Opcode = opcode;
            Expected = expected;
        }

        public static Sample Parse(string before, string opcode, string after)
        {
            var register = before.Split(new[] {"Before: ", "[", "]", ", ", " "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            var expected = after.Split(new[] {"After: ", "[", "]", ", ", " "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            var opcodeParts = opcode.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var opcodeNumber = int.Parse(opcodeParts[0]);
                
            var op = opcodeParts.Skip(1).Select(int.Parse).ToArray();

            return new Sample(register, opcodeNumber, op, expected);
        }
    }
}
