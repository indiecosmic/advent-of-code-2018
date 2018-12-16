using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day16
{
    class Program
    {
        static void Main()
        {
            var operations = new List<OpCode>
            {
                OpCode.Addi,
                OpCode.Addr,
                OpCode.Bani,
                OpCode.Banr,
                OpCode.Bori,
                OpCode.Borr,
                OpCode.Eqri,
                OpCode.Eqir,
                OpCode.Eqrr,
                OpCode.Gtri,
                OpCode.Gtir,
                OpCode.Gtrr,
                OpCode.Muli,
                OpCode.Mulr,
                OpCode.Seti,
                OpCode.Setr
            };

            var lines = Input.ReadRows();
            var samples = CreateSamples(lines);
            var part1Answer = CalculateSampleCount(samples, operations);
            Console.WriteLine($"Number of samples: {part1Answer}");

            var opCodesRegister = ResolveOpcodes(samples, operations);
            var testProgram = CreateTestProgram(lines);
            var register = RunTestProgram(opCodesRegister, testProgram);

            Console.WriteLine($"Register 0: {register[0]}");
            Console.ReadLine();
        }

        private static int[] RunTestProgram(IDictionary<int, OpCode> opCodesRegister, List<int[]> testProgram)
        {
            var register = new[] { 0, 0, 0, 0 };
            foreach (var instruction in testProgram)
            {
                var opNumber = instruction[0];
                var op = opCodesRegister[opNumber];
                register = op.Invoke(register, instruction);
            }

            return register;
        }

        private static List<int[]> CreateTestProgram(string[] lines)
        {
            var program = new List<int[]>();
            for (var i = 2370; i < lines.Length; i++) { 
                var op = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();
                program.Add(op);
            }
            return program;
        }

        private static int CalculateSampleCount(List<Sample> samples, List<OpCode> operations)
        {
            var count = 0;
            foreach (var sample in samples)
            {
                var opcodeCount = 0;
                foreach (var opCode in operations)
                {
                    if (opCode.Invoke(sample.Register, sample.Opcode).SameAs(sample.Expected))
                    {
                        opcodeCount++;
                    }
                }
                if (opcodeCount >= 3)
                    count++;
            }
            return count;
        }

        private static List<Sample> CreateSamples(string[] lines)
        {
            var samples = new List<Sample>();
            for (var i = 0; i < 2370; i += 3)
            {
                samples.Add(Sample.Parse(lines[i], lines[i + 1], lines[i + 2]));
            }
            return samples;
        }

        private static IDictionary<int, OpCode> ResolveOpcodes(List<Sample> samples, List<OpCode> opCodes)
        {
            var samplesRegister = samples.GroupBy(s => s.OpCodeNumber)
                .ToDictionary(g => g.Key, g => g.ToList());
            var result = new Dictionary<int, OpCode>();
            var remainingOpCodes = opCodes.ToList();
            while (result.Count < 16)
            {
                var remainingNumbers = samplesRegister.Keys.ToList();
                foreach (var number in remainingNumbers)
                {
                    var matching = FindMatchingOpCode(samplesRegister[number], remainingOpCodes);
                    if (matching.Count == 1)
                    {
                        var opCode = matching.First();
                        result.Add(number, opCode);
                        remainingOpCodes.Remove(opCode);
                        samplesRegister.Remove(number);
                    }
                }
            }
            return result;
        }

        private static List<OpCode> FindMatchingOpCode(List<Sample> samples, IEnumerable<OpCode> opCodes)
        {
            var matching = new List<OpCode>();
            foreach (var opCode in opCodes)
            {
                var found = samples.All(sample => opCode.Invoke(sample.Register, sample.Opcode).SameAs(sample.Expected));
                if (found)
                    matching.Add(opCode);
            }
            return matching;
        }
    }
}
