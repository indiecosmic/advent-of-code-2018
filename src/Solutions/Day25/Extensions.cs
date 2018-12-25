using System;
using System.Collections.Generic;
using System.Text;

namespace Day25
{
    public static class Extensions
    {
        public static int Distance(this (int x, int y, int z, int t) point, (int x, int y, int z, int t) other)
        {
            return Math.Abs(point.x - other.x) + Math.Abs(point.y - other.y) + Math.Abs(point.z - other.z) + Math.Abs(point.t - other.t);
        }
    }
}
