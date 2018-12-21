using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day20
{
    class Map
    {
        private readonly char[,] _grid;
        private readonly int _width;
        private readonly int _height;

        private Map(char[,] grid, int width, int height)
        {
            _grid = grid;
            _width = width;
            _height = height;
        }

        public static Map Create(List<Room> rooms)
        {
            var minX = rooms.Min(r => r.X);
            var minY = rooms.Min(r => r.Y);
            var maxX = rooms.Max(r => r.X);
            var maxY = rooms.Max(r => r.Y);

            var xOffset = 0 - minX;
            var yOffset = 0 - minY;
            var width = maxX + xOffset + 1;
            var height = maxY + yOffset + 1;
            width += width - 1;
            height += height - 1;

            width = width < 10 ? 10 : width;
            height = height < 10 ? 10 : height;

            var grid = new char[width, height];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    grid[x, y] = '#';
                }
            }

            for (var i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                var x = (room.X + xOffset) * 2;
                var y = (room.Y + yOffset) * 2;
                grid[x, y] = room.Start ? 'X': '.';

                foreach (var connected in room.Connected)
                {
                    if (connected.X < room.X)
                    {
                        grid[x - 1, y] = '|';
                    } else if (connected.X > room.X)
                    {
                        grid[x + 1, y] = '|';
                    } else if (connected.Y < room.Y)
                    {
                        grid[x, y - 1] = '-';
                    }
                    else
                    {
                        grid[x, y + 1] = '-';
                    }
                }
            }
            
            return new Map(grid, width, height);
        }

        public void Write()
        {
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    Console.Write(_grid[x,y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
