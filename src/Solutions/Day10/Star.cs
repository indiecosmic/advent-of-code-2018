using System;
using System.Collections.Generic;
using System.Text;

namespace Day10
{
    class Star
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Dx { get; }
        public int Dy { get; }


        public Star(int startX, int startY, int dx, int dy)
        {
            X = startX;
            Y = startY;
            Dx = dx;
            Dy = dy;
        }

        public void Tick()
        {
            X += Dx;
            Y += Dy;
        }
    }
}
