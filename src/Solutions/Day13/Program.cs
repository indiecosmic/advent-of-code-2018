using System;
using System.Linq;
using AdventOfCode.Common;

namespace Day13
{
    class Program
    {
        static void Main()
        {
            //Demo();
            //Demo2();
            var rows = Input.ReadRows(false);
            Part1(rows);
            Part2(rows);

            Console.ReadLine();
        }

        static void Part1(string[] rows)
        {
            var map = Map.Create(rows);
            while (!map.CollisionOccured)
            {
                map.Tick();
            }
        }

        static void Part2(string[] rows)
        {
            var map = Map.Create(rows, true);
            while (map.NumberOfCarts > 1)
            {
                map.Tick();
            }
            var lastCart = map.Carts.First();
            Console.WriteLine($"Last cart at: {lastCart.Position.X},{lastCart.Position.Y}");
        }

        static void Demo()
        {
            var input = @"/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   ";
            var rows = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var map = Map.Create(rows);
            map.Write();
            while (!map.CollisionOccured)
            {
                map.Tick();
                map.Write();

            }
            Console.ReadLine();
        }

        static void Demo2()
        {
            var input = @"/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/";
            var rows = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var map = Map.Create(rows, true);
            map.Write();
            while (map.NumberOfCarts > 1)
            {
                map.Tick();
                map.Write();
            }
            var lastCart = map.Carts.First();
            Console.WriteLine($"Last cart at: {lastCart.Position.X},{lastCart.Position.Y}");
        }
    }
}