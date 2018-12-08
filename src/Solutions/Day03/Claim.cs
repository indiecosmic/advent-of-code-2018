using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day03
{
    class Claim
    {
        public string Id { get; }
        public int Left { get; }
        public int Top { get; }
        public int Width { get; }
        public int Height { get; }

        private Rectangle _rect;

        public Claim(string input)
        {
            //#123 @ 3,2: 5x4
            var parts = input.Split(new []{' ', '@', ',', ':', 'x'}, StringSplitOptions.RemoveEmptyEntries);
            Id = parts[0];
            Left = Convert.ToInt32(parts[1]);
            Top = Convert.ToInt32(parts[2]);
            Width = Convert.ToInt32(parts[3]);
            Height = Convert.ToInt32(parts[4]);
            _rect = new Rectangle(Left, Top, Width, Height);
        }

        public bool Contains(int x, int y)
        {
            return _rect.Contains(x, y);
        }

        public bool Overlaps(Claim other)
        {
            return _rect.IntersectsWith(other._rect);
        }
    }
}
