using System.Collections.Generic;

namespace Day15
{
    class Tile
    {
        public Pawn Pawn { get; set; }
        public int X { get; }
        public int Y { get; }
        public char Symbol { get; }
        public List<Tile> Connected { get; } = new List<Tile>();

        public Tile(int x, int y, char symbol, Pawn pawn)
        {
            Pawn = pawn;
            X = x;
            Y = y;
            Symbol = symbol;
        }

        public bool IsWalkable => Symbol == '.';
        public bool IsOccupied => Pawn != null;

        public override string ToString()
        {
            return $"{X},{Y}: {Symbol}";
        }

        public void Connect(Tile neighbor)
        {
            Connected.Add(neighbor);
        }

        public int ReadingOrder(Tile other)
        {
            var yComp = Y.CompareTo(other.Y);
            if (yComp != 0) return yComp;
            var xComp = X.CompareTo(other.X);
            return xComp;
        }
    }
}
