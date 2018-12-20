using System;
using System.Collections.Generic;
using System.Text;

namespace Day19
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
    }
}
