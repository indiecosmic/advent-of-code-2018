using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day11
{
    public class FuelCell
    {
        public int RackId => Location.X + 10;
        public Point Location { get; }

        public FuelCell(Point location)
        {
            Location = location;
        }
    }
}
