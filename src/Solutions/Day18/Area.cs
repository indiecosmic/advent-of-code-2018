using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    class Area
    {
        public int WoodedAcres => CountAcres('|');
        public int Lumberyards => CountAcres('#');
        public int ResourceValue => WoodedAcres * Lumberyards;
        private readonly char[,] _acres;
        private readonly int _height;
        private readonly int _width;

        private Area(char[,] acres)
        {
            _acres = acres;
            _height = acres.GetLength(1);
            _width = acres.GetLength(0);
        }

        public static Area Create(string[] input)
        {
            var acres = new char[input[0].Length, input.Length];
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    acres[x, y] = input[y][x];
                }
            }
            return new Area(acres);
        }

        public Area Transform()
        {
            var destination = new char[_width, _height];
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    var acre = _acres[x, y];
                    var adjacent = GetAdjacent(x, y);
                    if (acre == '.')
                    {
                        destination[x, y] = adjacent.Count(a => a == '|') > 2 ? '|' : '.';
                    } else if (acre == '|')
                    {
                        destination[x, y] = adjacent.Count(a => a == '#') > 2 ? '#' : '|';
                    }
                    else
                    {
                        destination[x, y] = (adjacent.Any(a => a == '#') && adjacent.Any(a => a == '|')) ? '#' : '.';
                    }
                }
            }

            return new Area(destination);
        }

        private char[] GetAdjacent(int x, int y)
        {
            var xEdge = _width - 1;
            var yEdge = _height - 1;

            var adjacent = new List<char>();
            for (var row = y - 1; row <= y + 1; row++)
            {
                if (row < 0 || row > yEdge) continue;
                for (var col = x - 1; col <= x + 1; col++)
                {
                    if (row == y && col == x) continue;
                    if (col < 0 || col > xEdge) continue;

                    adjacent.Add(_acres[col, row]);
                }
            }
            return adjacent.ToArray();
        }

        private int CountAcres(char type)
        {
            var count = 0;
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (_acres[x, y] == type) count++;
                }
            }
            return count;
        }


        public void Write()
        {
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    Console.Write(_acres[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
