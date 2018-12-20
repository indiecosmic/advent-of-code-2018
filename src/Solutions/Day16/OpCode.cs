using System;

namespace Day16
{
    class OpCode
    {
        private readonly Func<int[], int[], int[]> _func;
        private readonly string _name;

        public OpCode(Func<int[], int[], int[]> func)
        {
            _func = func;
            _name = func.Method.Name;
        }

        public int[] Invoke(int[] register, int[] instruction)
        {
            return _func.Invoke(register, instruction);
        }

        public override string ToString()
        {
            return _name;
        }

        public static OpCode Addi => new OpCode(Operations.Addi);
        public static OpCode Addr => new OpCode(Operations.Addr);
        public static OpCode Bani => new OpCode(Operations.Bani);
        public static OpCode Banr => new OpCode(Operations.Banr);
        public static OpCode Bori => new OpCode(Operations.Bori);
        public static OpCode Borr => new OpCode(Operations.Borr);
        public static OpCode Eqri => new OpCode(Operations.Eqri);
        public static OpCode Eqir => new OpCode(Operations.Eqir);
        public static OpCode Eqrr => new OpCode(Operations.Eqrr);
        public static OpCode Gtri => new OpCode(Operations.Gtri);
        public static OpCode Gtir => new OpCode(Operations.Gtir);
        public static OpCode Gtrr => new OpCode(Operations.Gtrr);
        public static OpCode Mulr => new OpCode(Operations.Mulr);
        public static OpCode Muli => new OpCode(Operations.Muli);
        public static OpCode Seti => new OpCode(Operations.Seti);
        public static OpCode Setr => new OpCode(Operations.Setr);

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
