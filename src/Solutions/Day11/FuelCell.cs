using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day11
{
    public class FuelCell
    {
        public Point Location { get; }
        public int PowerLevel { get; }

        public FuelCell(Point location, int powerLevel)
        {
            Location = location;
            PowerLevel = powerLevel;
        }

        public static FuelCell Create(Point location, int gridSerialNumber)
        {
            var rackId = location.X + 10;
            var powerLevel = location.Y * rackId;
            powerLevel += gridSerialNumber;
            powerLevel *= rackId;
            powerLevel = (powerLevel / 100) % 10;
            powerLevel -= 5;

            return new FuelCell(location, powerLevel);
        }
    }
}
