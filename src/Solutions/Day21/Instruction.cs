using System;

namespace Day21
{
    class Instruction
    {
        public string Name { get; }
        public int[] Data { get; }

        public Instruction(string name, int[] data)
        {
            Name = name;
            Data = data;
        }

        public override string ToString()
        {
            return $"{Name} {Data[0]} {Data[1]} {Data[2]}";
        }

        public void Write()
        {
            Console.Write($"{Name} {Data[0]} {Data[1]} {Data[2]} ");
        }
    }
}
