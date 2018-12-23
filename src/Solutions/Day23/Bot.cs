using System;
using System.Collections.Generic;
using System.Text;

namespace Day23
{
    class Bot
    {
        public long X { get; }
        public long Y { get; }
        public long Z { get; }
        public long Range { get; }
        public (long min, long max) XRange { get; }

        public (long min, long max) YRange { get; set; }
        public (long min, long max) ZRange { get; set; }

        public Bot(long x, long y, long z, long range)
        {
            X = x;
            Y = y;
            Z = z;
            Range = range;
            XRange = (x - range, x + range);
            YRange = (y - range, y + range);
            ZRange = (z - range, z + range);
        }

        public bool InRangeOf(Bot strongest)
        {
            var distance = Math.Abs(X - strongest.X) + Math.Abs(Y - strongest.Y) + Math.Abs(Z - strongest.Z);
            return distance <= strongest.Range;
        }

        public bool InRange(long x, long y, long z)
        {
            var distance = Math.Abs(X - x) + Math.Abs(Y - y) + Math.Abs(Z - z);
            return distance <= Range;
        }

        public override string ToString()
        {
            return $"<{X},{Y},{Z}> r={Range}";
        }
    }
}
