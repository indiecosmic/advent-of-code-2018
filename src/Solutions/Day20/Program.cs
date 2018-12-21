using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day20
{
    class Program
    {
        static void Main()
        {
            
            var instructions = Input.ReadInput();// "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$";
            instructions = instructions.Replace("^", "").Replace("$", "");

            var start = new Room(1000, 1000, 0, true);
            var rooms = MapRooms(start, instructions);

            var map = Map.Create(rooms);
            map.Write();

            var part1Answer = rooms.Max(r => r.Distance);
            Console.WriteLine($"Max number of doors: {part1Answer}");
            var part2Answer = rooms.Count(r => r.Distance >= 1000);
            Console.WriteLine($"Number of rooms above 1000: {part2Answer}");

            Console.ReadLine();
        }



        static List<Room> MapRooms(Room start, string instructions)
        {
            var previousRooms = new Stack<Room>();
            var rooms = new List<Room>{start};
            var currentRoom = start;
            foreach (var instruction in instructions)
            {
                if (instruction == 'E' || instruction == 'W' || instruction == 'N' || instruction == 'S')
                {
                    currentRoom = Create(currentRoom, instruction, rooms);
                }
                else if (instruction == '(')
                {
                    previousRooms.Push(currentRoom);
                }
                else if (instruction == '|')
                {
                    currentRoom = previousRooms.Peek();
                }
                else if (instruction == ')')
                {
                    previousRooms.Pop();
                }
            }
            return rooms;
        }

        static Room Create(Room currentRoom, char direction, List<Room> rooms)
        {
            var x = currentRoom.X;
            var y = currentRoom.Y;
            switch (direction)
            {
                case 'E':
                    x++;
                    break;
                case 'W':
                    x--;
                    break;
                case 'N':
                    y--;
                    break;
                case 'S':
                    y++;
                    break;
            }
            var next = rooms.FirstOrDefault(r => r.X == x && r.Y == y);
            if (next == null)
            {
                next = new Room(x, y, currentRoom.Distance + 1);
                rooms.Add(next);
            }
            currentRoom.ConnectTo(next);
            return next;
        }
    }
}
