using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Map
    {
        private bool GameOver
        {
            get { return _elves.All(e => e.Dead) || _goblins.All(g => g.Dead); }
        }

        public int Ticks { get; private set; }

        public int RemainingHitPoints => _elves.All(e => e.Dead) ? _goblins.Where(g => !g.Dead).Sum(g => g.HitPoints) : _elves.Where(e => !e.Dead).Sum(e => e.HitPoints);
        public int Casualties => _elves.Count(e => e.Dead);
        public Pawn.Factions Winner => _elves.Any(e => !e.Dead) ? Pawn.Factions.Elf : Pawn.Factions.Goblin;

        private readonly Tile[,] _tiles;
        private readonly List<Pawn> _elves;
        private readonly List<Pawn> _goblins;
        private readonly int _height;
        private readonly int _width;

        public Map(Tile[,] tiles, List<Pawn> elves, List<Pawn> goblins)
        {
            _tiles = tiles;
            _elves = elves;
            _goblins = goblins;
            _height = tiles.GetLength(1);
            _width = tiles.GetLength(0);

            ConnectTiles();
        }

        private void ConnectTiles()
        {
            foreach (var tile in _tiles)
            {
                var adjacent = new[]
                {
                    TileAt(tile.X, tile.Y - 1),
                    TileAt(tile.X - 1, tile.Y),
                    TileAt(tile.X + 1, tile.Y),
                    TileAt(tile.X, tile.Y + 1)
                };
                foreach (var neighbor in adjacent)
                {
                    if (neighbor == null) continue;
                    if (neighbor.IsWalkable)
                        tile.Connect(neighbor);
                }
            }
        }



        public static Map Create(string[] input, int elfAttackPower = 3)
        {
            var grid = new Tile[input[0].Length, input.Length];
            var elves = new List<Pawn>();
            var goblins = new List<Pawn>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    var tile = input[y][x];
                    if (tile == 'G')
                    {
                        var pawn = new Pawn(Pawn.Factions.Goblin, x, y, 3);
                        grid[x, y] = new Tile(x, y, '.', pawn);
                        goblins.Add(pawn);
                    }
                    else if (tile == 'E')
                    {
                        var pawn = new Pawn(Pawn.Factions.Elf, x, y, elfAttackPower);
                        grid[x,y] = new Tile(x, y, '.', pawn);
                        elves.Add(pawn);
                    }
                    else
                    {
                        grid[x, y] = new Tile(x, y, tile, null);
                    }
                }
            }

            return new Map(grid, elves, goblins);
        }

        public IList<Pawn> GetPawnsOfType(Pawn.Factions faction)
        {
            return faction == Pawn.Factions.Elf ? _elves : _goblins;
        }

        public bool Update()
        {
            var movingOrder = _elves.Union(_goblins).Where(p => !p.Dead).OrderBy(p => p.Y).ThenBy(p => p.X);
            foreach (var pawn in movingOrder)
            {
                pawn.Update(this);
                if (GameOver) return false;
            }
            Ticks++;
            return !GameOver;
        }

        public void Write()
        {
            for (var y = 0; y < _height; y++)
            {
                var pawnsAtRow = new List<Pawn>();
                for (var x = 0; x < _width; x++)
                {
                    var tile = _tiles[x, y];
                    if (tile.Pawn != null)
                    {
                        Console.Write(tile.Pawn.Faction == Pawn.Factions.Elf ? 'E' : 'G');
                        pawnsAtRow.Add(tile.Pawn);
                    }
                    else
                    {
                        Console.Write(_tiles[x, y].Symbol);
                    }
                }
                foreach (var p in pawnsAtRow)
                    Console.Write($" {p.InfoString}, ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public Tile TileAt(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
                return null;
            return _tiles[x, y];
        }
    }
}
