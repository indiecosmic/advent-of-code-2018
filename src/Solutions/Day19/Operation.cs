using System;

namespace Day19
{
    class Operation
    {
        private readonly Func<int[], int[], int[]> _func;
        public readonly string Name;

        public Operation(Func<int[], int[], int[]> func)
        {
            _func = func;
            Name = func.Method.Name;
        }

        public int[] Invoke(int[] register, int[] instruction)
        {
            return _func.Invoke(register, instruction);
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
            public static int[] Addr(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] + register[registerB];
                return output;
            }

            public static int[] Addi(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] + valueB;
                return output;
            }

            public static int[] Mulr(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] * register[registerB];
                return output;
            }
            public static int[] Muli(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] * valueB;
                return output;
            }

            public static int[] Banr(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] & register[registerB];
                return output;
            }

            public static int[] Bani(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] & valueB;
                return output;
            }

            public static int[] Borr(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] | register[registerB];
                return output;
            }

            public static int[] Bori(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] | valueB;
                return output;
            }

            public static int[] Setr(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var registerC = instruction[2];
                output[registerC] = register[registerA];
                return output;
            }

            public static int[] Seti(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var valueA = instruction[0];
                var registerC = instruction[2];
                output[registerC] = valueA;
                return output;
            }

            public static int[] Gtir(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var valueA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = valueA > register[registerB] ? 1 : 0;
                return output;
            }
            public static int[] Gtri(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] > valueB ? 1 : 0;
                return output;
            }
            public static int[] Gtrr(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] > register[registerB] ? 1 : 0;
                return output;
            }

            public static int[] Eqir(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var valueA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = valueA == register[registerB] ? 1 : 0;
                return output;
            }
            public static int[] Eqri(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var valueB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] == valueB ? 1 : 0;
                return output;
            }
            public static int[] Eqrr(int[] register, int[] instruction)
            {
                var output = (int[])register.Clone();
                var registerA = instruction[0];
                var registerB = instruction[1];
                var registerC = instruction[2];
                output[registerC] = register[registerA] == register[registerB] ? 1 : 0;
                return output;
            }
        }
    }
}
