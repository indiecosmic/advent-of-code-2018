using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Pawn
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int HitPoints { get; private set; } = 200;
        public int AttackPower { get; }
        public bool Dead => HitPoints <= 0;
        public Factions Faction { get; }
        public string InfoString => $"{(Faction == Factions.Elf ? 'E':'G')}({HitPoints})";

        public Pawn(Factions faction, int x, int y, int attackPower)
        {
            Faction = faction;
            X = x;
            Y = y;
            AttackPower = attackPower;
        }

        public void Update(Map map)
        {
            if (Dead) return;

            Move(map);
            Attack(map);
        }

        private void Attack(Map map)
        {
            var currentTile = map.TileAt(X, Y);
            var enemies = FindTargets(map);
            if (enemies.Length == 0) return;

            var attackableEnemies = enemies.Where(e => e.GetAdjacentTiles(map).Contains(currentTile)).ToArray();
            if (attackableEnemies.Length == 0) return;

            var selectedEnemy = attackableEnemies.OrderBy(e => e.HitPoints).ThenBy(e => e.Y).ThenBy(e => e.X).First();
            Attack(selectedEnemy);

            if (selectedEnemy.Dead)
            {
                map.TileAt(selectedEnemy.X, selectedEnemy.Y).Pawn = null;
                selectedEnemy.X = -1;
                selectedEnemy.Y = -1;
            }
        }

        private void Attack(Pawn enemy)
        {
            enemy.HitPoints -= AttackPower;
        }
        
        private void Move(Map map)
        {
            var currentTile = map.TileAt(X, Y);
            var enemies = FindTargets(map);
            var enemyTiles = enemies.SelectMany(e => e.GetAdjacentTiles(map)).ToArray();
            if (enemyTiles.Contains(currentTile))
                return;
            enemyTiles = enemyTiles.Where(t => t.IsWalkable).ToArray();

            var reachableTiles = FindReachableTiles(currentTile);
            var reachableEnemyTiles = enemyTiles.Where(t => reachableTiles.Contains(t)).ToArray();
            if (reachableEnemyTiles.Length == 0) return;
            var nextMove = CalculateNextMove(currentTile, reachableTiles, reachableEnemyTiles);

            currentTile.Pawn = null;
            nextMove.Pawn = this;
            X = nextMove.X;
            Y = nextMove.Y;
        }

        private Tile CalculateNextMove(Tile sourceTile, List<Tile> reachableTiles, Tile[] reachableEnemyTiles)
        {
            var shortestPathSet = new List<Tile>();
            var origins = reachableTiles.ToDictionary(t => t, t => null as Tile);
            var distances = reachableTiles.ToDictionary(t => t, t => int.MaxValue);
            distances[sourceTile] = 0;
            while (shortestPathSet.Count < distances.Count)
            {
                var tile = distances.Where(t => !shortestPathSet.Contains(t.Key)).OrderBy(t => t.Value).FirstOrDefault().Key;
                shortestPathSet.Add(tile);
                foreach (var connected in tile.Connected)
                {
                    if (distances.ContainsKey(connected) && !shortestPathSet.Contains(connected) && distances[connected] >= distances[tile] + 1) { 
                        
                        if (distances[connected] > distances[tile] + 1) { 
                            origins[connected] = tile;
                            distances[connected] = distances[tile] + 1;
                        }
                        else
                        {
                            if (origins[connected] == null)
                                origins[connected] = tile;
                            else if (origins[connected].ReadingOrder(tile) > 0)
                                origins[connected] = tile;
                        }
                    }
                }
            }

            var closestEnemy = distances.Where(d => reachableEnemyTiles.Contains(d.Key)).OrderBy(d => d.Value)
                .ThenBy(d => d.Key.Y).ThenBy(d => d.Key.X).FirstOrDefault().Key;
            var next = origins[closestEnemy];
            var previous = closestEnemy;
            while (next != sourceTile)
            {
                previous = next;
                next = origins[next];
            }

            return previous;
        }

        private List<Tile> FindReachableTiles(Tile sourceTile)
        {
            var discoveredTiles = new List<Tile>();
            FindReachable(sourceTile);
            return discoveredTiles;

            void FindReachable(Tile source)
            {
                discoveredTiles.Add(source);
                foreach (var tile in source.Connected)
                {
                    if (!tile.IsOccupied && !discoveredTiles.Contains(tile))
                        FindReachable(tile);
                }
            }
        }

        private Pawn[] FindTargets(Map map)
        {
            return map.GetPawnsOfType(Faction == Factions.Elf ? Factions.Goblin : Factions.Elf).Where(p => !p.Dead).ToArray();
        }

        private Tile[] GetAdjacentTiles(Map map)
        {
            return new[]
            {
                map.TileAt(X, Y - 1),
                map.TileAt(X - 1, Y),
                map.TileAt(X + 1, Y),
                map.TileAt(X, Y + 1)
            };
        }

        public enum Factions
        {
            Elf,
            Goblin
        }
    }
}
