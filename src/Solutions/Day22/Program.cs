using System;
using System.Collections.Generic;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            var depth = 3339;
            var targetX = 10;
            var targetY = 715;
            var width = targetX + 1;
            var height = targetY + 1;

            var map = new char[width, height];
            var erosion = new int[width, height];
            var risk = new int[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    risk[x, y] = Risk(x, y, erosion, targetX, targetY, depth);
                    if (x == 0 && y == 0)
                    {
                        map[x, y] = 'M';
                        continue;
                    }
                    if (x == targetX && y == targetY)
                    {
                        map[x, y] = 'T';
                        continue;
                    }
                    map[x, y] = (risk[x, y] == 0) ? '.' : risk[x, y] == 1 ? '=' : '|';
                }
            }

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }

            var part1Answer = CalculateTotalRiskLevel(risk, width, height);

            Console.WriteLine($"Total risk level: {part1Answer}");
            Console.ReadLine();

        }

        static int CalculateTotalRiskLevel(int[,] risk, int width, int height)
        {
            var riskSum = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    riskSum += risk[x, y];
                }
            }
            return riskSum;
        }

        static int Risk(int x, int y, int[,] erosionValues, int targetX, int targetY, int depth)
        {
            var geologicIndex = 0;

            if ((x == 0 && y == 0) || (x == targetX && y == targetY)) geologicIndex = 0;
            else if (y == 0) geologicIndex = x * 16807;
            else if (x == 0) geologicIndex = y * 48271;
            else geologicIndex = erosionValues[x - 1, y] * erosionValues[x, y - 1];
            var erosion = (geologicIndex + depth) % 20183;
            erosionValues[x, y] = erosion;
            return erosion % 3;
        }

        static IEnumerable<Gear> AllowedGear(int x, int y, int[,] risk, int targetX, int targetY)
        {
            if (x == targetX && y == targetY) return new[] { Gear.Torch };
            switch (risk[x, y])
            {
                case 0:
                    return new[] { Gear.Climbing, Gear.Torch };
                case 1:
                    return new[] { Gear.None, Gear.Climbing };
                case 2:
                default:
                    return new[] { Gear.None, Gear.Torch };
            }
        }

        enum Gear
        {
            None,
            Climbing,
            Torch
        }
    }
}
