using System;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            var depth = 510;
            var targetX = 10;
            var targetY = 10;
            var width = 20;
            var height = 20;

            var erosion = new int[width, height];
            var risk = new int[width, height];
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    risk[x, y] = Risk(x, y, erosion, targetX, targetY, depth);
                }
            }
            var part1Answer = CalculateTotalRiskLevel(risk, targetX, targetY);
            var part2Answer = ShortestPath(risk, 0, 0, (x: targetX, y: targetY));

            Console.WriteLine($"Total risk level: {part1Answer}");
            Console.ReadLine();

        }

        static int CalculateTotalRiskLevel(int[,] risk, int width, int height)
        {
            var riskSum = 0;
            for (var y = 0; y <= height; y++)
            {
                for (var x = 0; x <= width; x++)
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

        static bool AllowedGear(int x, int y, int g, int[,] risk, int targetX, int targetY)
        {
            switch (risk[x, y])
            {
                case 0:
                    return g == (int)Gear.Climbing || g == (int)Gear.Torch;
                case 1:
                    return g == (int)Gear.None || g == (int)Gear.Climbing;
                case 2:
                default:
                    return g == (int)Gear.None || g == (int)Gear.Torch;
            }
        }

        static int ShortestPath(int[,] map, int sourceX, int sourceY, (int x, int y) target)
        {
            var north = (x: 0, y: -1);
            var south = (x: 0, y: 1);
            var west = (x: -1, y: 0);
            var east = (x: 1, y: 0);

            var width = map.GetLength(0);
            var height = map.GetLength(1);

            var distances = new int[width, height, 3];
            var visited = new bool[width, height, 3];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    for (var g = 0; g < 3; g++)
                    {
                        distances[x, y, g] = int.MaxValue;
                        visited[x, y, g] = false;
                    }
                }
            }

            distances[sourceX, sourceY, (int)Gear.Torch] = 0;
            while (true)
            {
                var region = MinDistance(distances, visited);
                if (region == (-1, -1, -1)) return distances[target.x, target.y, 2];

                visited[region.x, region.y, region.g] = true;

                SetDistance(region, (region.x + north.x, region.y + north.y, region.g));
                SetDistance(region, (region.x + south.x, region.y + south.y, region.g));
                SetDistance(region, (region.x + west.x, region.y + west.y, region.g));
                SetDistance(region, (region.x + east.x, region.y + east.y, region.g));
                SetTool(region, 0);
                SetTool(region, 1);
                SetTool(region, 2);

            }

            void SetDistance((int x, int y, int g) source, (int x, int y, int g) destination)
            {
                if (destination.x < 0 || destination.x >= width || destination.y < 0 ||
                    destination.y >= height) return;

                //var travelCost = TravelCost(source, destination);
                if (!AllowedGear(destination.x, destination.y, destination.g, map, target.x, target.y))
                {
                    return;
                }

                if (!visited[destination.x, destination.y, destination.g] &&
                    distances[source.x, source.y, source.g] != int.MaxValue &&
                    distances[destination.x, destination.y, destination.g] + 1 < distances[source.x, source.y, source.g])
                {
                    distances[destination.x, destination.y, destination.g] = distances[source.x, source.y, destination.g] + 1;
                }
            }

            void SetTool((int x, int y, int g) source, int g)
            {
                if (source.g == g) return;

                if (!AllowedGear(source.x, source.y, g, map, target.x, target.y))
                {
                    visited[source.x, source.y, g] = true;
                    return;
                }
                var d = distances[source.x, source.y, source.g];

                if (distances[source.x, source.y, g] > d + 7)
                {
                    distances[source.x, source.y, g] = 7 + d;
                }
            }
        }

        private static (int x, int y, int g) MinDistance(int[,,] distances, bool[,,] visited)
        {
            var width = distances.GetLength(0);
            var height = distances.GetLength(1);
            int min = int.MaxValue;
            var minRegion = (-1, -1, -1);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    for (var g = 0; g < 3; g++)
                    {
                        if (visited[x, y, g] == false && distances[x, y, g] <= min)
                        {
                            min = distances[x, y, g];
                            minRegion = (x, y, g);
                        }
                    }
                }
            }

            return minRegion;
        }

        enum Gear
        {
            None = 0,
            Climbing = 1,
            Torch = 2
        }
    }
}
