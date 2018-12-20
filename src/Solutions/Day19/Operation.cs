using System;

namespace Day19
{
    class Operation
    {
        private readonly Action<int[], int[]> _action;
        public readonly string Name;

        private Operation(Action<int[], int[]> action)
        {
            _action = action;
            Name = action.Method.Name;
        }

        public void Invoke(int[] register, int[] instruction)
        {
            _action.Invoke(register, instruction);
        }

        public override string ToString()
        {
            return Name;
        }

        public static Operation Addi => new Operation(Operations.Addi);
        public static Operation Addr => new Operation(Operations.Addr);
        public static Operation Bani => new Operation(Operations.Bani);
        public static Operation Banr => new Operation(Operations.Banr);
        public static Operation Bori => new Operation(Operations.Bori);
        public static Operation Borr => new Operation(Operations.Borr);
        public static Operation Eqri => new Operation(Operations.Eqri);
        public static Operation Eqir => new Operation(Operations.Eqir);
        public static Operation Eqrr => new Operation(Operations.Eqrr);
        public static Operation Gtri => new Operation(Operations.Gtri);
        public static Operation Gtir => new Operation(Operations.Gtir);
        public static Operation Gtrr => new Operation(Operations.Gtrr);
        public static Operation Mulr => new Operation(Operations.Mulr);
        public static Operation Muli => new Operation(Operations.Muli);
        public static Operation Seti => new Operation(Operations.Seti);
        public static Operation Setr => new Operation(Operations.Setr);

        private static class Operations
        {
            public static void Addr(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] + register[registerB];
            }

            public static void Addi(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] + valueB;
            }

            public static void Mulr(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] * register[registerB];

            }
            public static void Muli(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] * valueB;
            }

            public static void Banr(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] & register[registerB];
            }

            public static void Bani(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] & valueB;
            }

            public static void Borr(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] | register[registerB];
            }

            public static void Bori(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] | valueB;
            }

            public static void Setr(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var registerC = instruction[2];
                register[registerC] = register[registerA];
            }

            public static void Seti(int[] register, int[] instruction)
            {
                var valueA = instruction[0];
                var registerC = instruction[2];
                register[registerC] = valueA;
            }

            public static void Gtir(int[] register, int[] instruction)
            {
                var valueA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = valueA > register[registerB] ? 1 : 0;
            }
            public static void Gtri(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] > valueB ? 1 : 0;
            }
            public static void Gtrr(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] > register[registerB] ? 1 : 0;
            }

            public static void Eqir(int[] register, int[] instruction)
            {
                var valueA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = valueA == register[registerB] ? 1 : 0;
            }
            public static void Eqri(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] == valueB ? 1 : 0;
            }
            public static void Eqrr(int[] register, int[] instruction)
            {
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                register[registerC] = register[registerA] == register[registerB] ? 1 : 0;
            }
        }
    }
}
