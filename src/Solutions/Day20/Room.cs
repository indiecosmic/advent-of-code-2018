using System;
using System.Collections.Generic;
using System.Text;

namespace Day20
{
    class Room
    {
        public int X { get; }
        public int Y { get; }
        public int Distance { get; }
        public bool Start { get; }
        public List<Room> Connected { get; } = new List<Room>();

        public Room(int x, int y, int distance, bool start = false)
        {
            X = x;
            Y = y;
            Distance = distance;
            Start = start;
        }

        public void ConnectTo(Room other)
        {
            if (!Connected.Contains(other))
                Connected.Add(other);
            if (!other.Connected.Contains(this))
                other.Connected.Add(this);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
