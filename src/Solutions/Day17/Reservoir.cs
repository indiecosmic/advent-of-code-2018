using System;

namespace Day17
{
    class Reservoir
    {
        readonly char[,] _grid;
        readonly int _maxY;
        readonly int _minY = int.MaxValue;

        public int WaterTiles
        {
            get
            {
                var count = 0;
                for (var y = _minY; y < _grid.GetLength(1); y++)
                {
                    for (var x = 0; x < _grid.GetLength(0); x++)
                    {
                        if (_grid[x, y] == 'W')
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        public int SandWithWater
        {
            get
            {
                var count = 0;
                for (var y = _minY; y < _grid.GetLength(1); y++)
                {
                    for (var x = 0; x < _grid.GetLength(0); x++)
                    {
                        if (_grid[x, y] == '|')
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        public Reservoir(string[] input)
        {
            var x = 2000;
            var y = 2100;

            _grid = new char[x, y];

            foreach (var line in input)
            {
                var l = line.Split(new[] { '=', ',', '.' });

                if (l[0] == "x")
                {
                    x = int.Parse(l[1]);
                    y = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var a = y; a <= len; a++)
                    {
                        _grid[x, a] = '#';
                    }
                }
                else
                {
                    y = int.Parse(l[1]);
                    x = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var a = x; a <= len; a++)
                    {
                        _grid[a, y] = '#';
                    }
                }

                if (y > _maxY)
                {
                    _maxY = y;
                }

                if (y < _minY)
                {
                    _minY = y;
                }
            }
        }

        public void Go()
        {
            var springX = 500;
            var springY = 0;

            // fill with water
            GoDown(springX, springY);
        }

        private bool SpaceTaken(int x, int y)
        {
            return _grid[x, y] == '#' || _grid[x, y] == 'W';
        }

        private void GoDown(int x, int y)
        {
            _grid[x, y] = '|';
            while (_grid[x, y + 1] != '#' && _grid[x, y + 1] != 'W')
            {

                y++;
                if (y > _maxY)
                {
                    return;
                }
                _grid[x, y] = '|';
            }

            do
            {
                bool goDownLeft = false;
                bool goDownRight = false;

                // find boundaries
                int minX;
                for (minX = x; minX >= 0; minX--)
                {
                    if (SpaceTaken(minX, y + 1) == false)
                    {
                        goDownLeft = true;
                        break;
                    }

                    _grid[minX, y] = '|';

                    if (SpaceTaken(minX - 1, y))
                    {
                        break;
                    }

                }

                int maxX;
                for (maxX = x; maxX < _grid.GetLength(0); maxX++)
                {
                    if (SpaceTaken(maxX, y + 1) == false)
                    {
                        goDownRight = true;

                        break;
                    }

                    _grid[maxX, y] = '|';

                    if (SpaceTaken(maxX + 1, y))
                    {
                        break;
                    }

                }

                // handle water falling
                if (goDownLeft)
                {
                    GoDown(minX, y);
                }

                if (goDownRight)
                {
                    GoDown(maxX, y);
                }

                if (goDownLeft || goDownRight)
                {
                    return;
                }

                // fill row
                for (int a = minX; a < maxX + 1; a++)
                {
                    _grid[a, y] = 'W';
                }

                y--;
            }
            while (true);
        }
    }
}
