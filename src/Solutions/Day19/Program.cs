using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
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
            Console.WriteLine($"Value in register 0: {part1Answer}");

            var part2Answer = CalculatePart2Answer(jumpRegister, instructions, operations);
            Console.WriteLine($"Value in register 0: {part2Answer}");

            Console.ReadLine();
        }



        private static int CalculatePart1Answer(int jumpRegister, List<Instruction> instructions, Dictionary<string, Operation> operations)
        {
            var register = new int[6];
            var instructionPointer = register[jumpRegister];
            while (true)
            {
                var instruction = instructions[instructionPointer];
                register = operations[instruction.Name].Invoke(register, instruction.Data);
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
            register[0] = 1;
            var instructionPointer = register[jumpRegister];
            while (true)
            {
                if (instructionPointer == 3)
                {
                    if (register[2] == 1)
                        register[2] = (register[5] / register[1]);
                    else if (register[2] > register[5] / register[1])
                        register[2] = register[5];
                }
                var instruction = instructions[instructionPointer];
                var result = operations[instruction.Name].Invoke(register, instruction.Data);
                instructionPointer = result[jumpRegister];
                instructionPointer++;
                if (instructionPointer < 0 || instructionPointer >= instructions.Count)
                    break;
                result[jumpRegister] = instructionPointer;
                //Console.WriteLine($"[{register[0]},{register[1]},{register[2]},{register[3]},{register[4]},{register[5]}] {instruction} [{result[0]},{result[1]},{result[2]},{result[3]},{result[4]},{result[5]}]");
                register = result;
            }
            return register[0];
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
    }
}
