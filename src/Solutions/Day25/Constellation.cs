using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day25
{
    class Constellation
    {
        private List<(int x, int y, int z, int t)> _points;

        public Constellation((int x, int y, int z, int t) point)
        {
            _points = new List<(int x, int y, int z, int t)> {point};
        }

        public bool CanJoin((int x, int y, int z, int t) point)
        {
            return _points.Any(p => p.Distance(point) <= 3);
        }

        public void Join((int x, int y, int z, int t) point)
        {
            _points.Add(point);
        }
    }
}
