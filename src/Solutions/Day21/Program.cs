using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day21
{
    class Program
    {
        static void Main()
        {
            var operations = new[]
            {
                Operation.Addi,
                Operation.Addr,
                Operation.Bani,
                Operation.Banr,
                Operation.Bori,
                Operation.Borr,
                Operation.Eqri,
                Operation.Eqir,
                Operation.Eqrr,
                Operation.Gtri,
                Operation.Gtir,
                Operation.Gtrr,
                Operation.Muli,
                Operation.Mulr,
                Operation.Seti,
                Operation.Setr
            }.ToDictionary(i => i.Name.ToLower(), i => i);

            var input = Input.ReadRows();
            var jumpRegister = (int)char.GetNumericValue(input[0][4]);
            var instructions = CreateInstructions(input.Skip(1));
            var part1Answer = CalculatePart1Answer(jumpRegister, instructions, operations);
            Console.WriteLine($"Value for register 0 fewest instructions: {part1Answer}");
            var part2Answer = CalculatePart2Answer(jumpRegister, instructions, operations);
            Console.WriteLine($"Value for register 0 most instructions: {part2Answer}");
            Console.ReadLine();
        }

        private static List<Instruction> CreateInstructions(IEnumerable<string> input)
        {
            var instructions = new List<Instruction>();
            foreach (var line in input)
            {
                var parts = line.Split(" ");
                instructions.Add(new Instruction(parts[0], parts.Skip(1).Select(int.Parse).ToArray()));
            }
            return instructions;
        }

        private static int CalculatePart1Answer(int jumpRegister, List<Instruction> instructions, Dictionary<string, Operation> operations)
        {
            var register = new int[6];
            var instructionPointer = register[jumpRegister];
            while (true)
            {
                var instruction = instructions[instructionPointer];
                if (instructionPointer == 28)
                {
                    var copyFrom = instruction.Data[0] != 0 ? instruction.Data[0] : instruction.Data[1];
                    register[0] = register[copyFrom];
                }
                operations[instruction.Name].Invoke(register, instruction.Data);
                instructionPointer = register[jumpRegister] + 1;
                if (instructionPointer < 0 || instructionPointer >= instructions.Count)
                    break;
                register[jumpRegister] = instructionPointer;
            }
            return register[0];
        }

        private static int CalculatePart2Answer(int jumpRegister, List<Instruction> instructions, Dictionary<string, Operation> operations)
        {
            var register = new int[6];
            var instructionPointer = register[jumpRegister];
            var history = new List<int>();
            while (true)
            {
                var instruction = instructions[instructionPointer];
                if (instructionPointer == 28)
                {
                    var copyFrom = instruction.Data[0] != 0 ? instruction.Data[0] : instruction.Data[1];
                    var value = register[copyFrom];
                    if (history.Contains(value))
                        return history[history.Count - 1];
                    history.Add(value);
                }
                operations[instruction.Name].Invoke(register, instruction.Data);
                instructionPointer = register[jumpRegister] + 1;
                if (instructionPointer < 0 || instructionPointer >= instructions.Count)
                    break;
                register[jumpRegister] = instructionPointer;
            }
            return register[0];
        }
    }
}
