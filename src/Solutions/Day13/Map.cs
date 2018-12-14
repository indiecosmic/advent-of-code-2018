using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class Map
    {
        public bool CollisionOccured { get; private set; }
        public int NumberOfCarts => Carts.Count;

        private readonly char[,] _tracks;
        public List<Cart> Carts { get; }
        private readonly bool _removeCartsOnCollision;
        private int _ticks;

        private Map(char[,] tracks, Cart[] carts, bool removeCartsOnCollision)
        {
            _tracks = tracks;
            Carts = new List<Cart>(carts);
            _removeCartsOnCollision = removeCartsOnCollision;
        }

        public static Map Create(string[] rows, bool removeCartsOnCollision = false)
        {
            var height = rows.Length;
            var width = rows[0].Length;
            var tracks = new char[width, height];
            var carts = new List<Cart>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var track = rows[y][x];
                    if (track == '<' || track == '>')
                    {
                        tracks[x, y] = '-';
                        carts.Add(Cart.Create(track, x, y));
                    }
                    else if (track == '^' || track == 'v')
                    {
                        tracks[x, y] = '|';
                        carts.Add(Cart.Create(track, x, y));
                    }
                    else
                    {
                        tracks[x, y] = track;
                    }
                }
            }
            return new Map(tracks, carts.ToArray(), removeCartsOnCollision);
        }

        public void Tick()
        {
            foreach (var cart in Carts.OrderBy(c => c.Position.Y).ThenBy(c => c.Position.X))
            {
                cart.Update(_tracks);
                var collisions = Carts.GroupBy(c => c.Position)
                    .Where(g => g.Count() > 1)
                    .ToDictionary(g => g.Key, g => g.ToList());
                if (collisions.Count > 0)
                {
                    if (!_removeCartsOnCollision)
                    {
                        Console.WriteLine($"Collision at {collisions.First()} at tick {_ticks}");
                        CollisionOccured = true;
                        return;
                    }
                    foreach (var collidingCart in collisions.SelectMany(c => c.Value))
                    {
                        Carts.Remove(collidingCart);
                    }
                }
            }

            _ticks++;
        }

        public void Write()
        {
            var map = (char[,])_tracks.Clone();
            foreach (var cart in Carts)
            {
                map[cart.Position.X, cart.Position.Y] = cart.Symbol;
            }
            if (CollisionOccured)
            {
                var collision = Carts.GroupBy(c => c.Position)
                    .Where(g => g.Count() > 1)
                    .Select(c => c.Key).FirstOrDefault();
                map[collision.X, collision.Y] = 'X';
            }
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(_ticks);
        }
    }
}
