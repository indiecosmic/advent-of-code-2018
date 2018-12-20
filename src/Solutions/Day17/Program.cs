using System;
using AdventOfCode.Common;

namespace Day17
{
    class Program
    {
        static void Main()
        {
            var lines = Input.ReadRows();
            Reservoir reservoir = new Reservoir(lines);
            
            reservoir.Go();

            var part1Answer = reservoir.SandWithWater + reservoir.WaterTiles;
            Console.WriteLine($"Water tiles reached: {part1Answer}");

            var part2Answer = reservoir.WaterTiles;
            Console.WriteLine($"Water tiles left: {part2Answer}");

            Console.ReadLine();
        }


    }
}
