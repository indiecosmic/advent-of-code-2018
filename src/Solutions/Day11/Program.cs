using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day11
{
    class Program
    {
        static void Main()
        {
            const int size = 300;
            const int serialNumber = 3628;
            var grid = CreateGrid(size, serialNumber);
            var part1Answer = FindArea(grid, size, 3);
            Console.WriteLine($"Coordinate: {part1Answer.Item1.Location.X},{part1Answer.Item1.Location.Y} (power level: {part1Answer.Item3})");

            (FuelCell, int, int) max = (null, 0, 0);
            for (var i = 1; i <= size; i++)
            {
                var result = FindArea(grid, size, i);
                if (result.Item3 < 0) break;
                if (result.Item3 > max.Item3)
                    max = result;
            }

            Console.WriteLine($"Identifier: {max.Item1.Location.X},{max.Item1.Location.Y},{max.Item2}");
            Console.ReadLine();
        }

        private static (FuelCell, int, int) FindArea(FuelCell[,] grid, int gridSize, int squareSize)
        {
            var maxLevel = int.MinValue;
            FuelCell fuelCell = null;
            for (var x = 0; x < gridSize - (squareSize + 1); x++)
            {
                for (var y = 0; y < gridSize - (squareSize + 1); y++)
                {
                    var powerLevel = TotalPowerLevel(grid, x, y, squareSize);
                    if (powerLevel > maxLevel)
                    {
                        maxLevel = powerLevel;
                        fuelCell = grid[x, y];
                    }
                }
            }

            return (fuelCell, squareSize, maxLevel);
        }

        private static int TotalPowerLevel(FuelCell[,] grid, int topLeftX, int topleftY, int squareSize)
        {
            var result = 0;
            for (var x = topLeftX; x < topLeftX + squareSize; x++)
            {
                for (var y = topleftY; y < topleftY + squareSize; y++)
                {
                    result += grid[x, y].PowerLevel;
                }
            }
            return result;
        }

        private static FuelCell[,] CreateGrid(int size, int input)
        {
            var grid = new FuelCell[size, size];
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    grid[x, y] = FuelCell.Create(new Point(x+1, y+1), input);
                }
            }
            return grid;
        }
    }
}
