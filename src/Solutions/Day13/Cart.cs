using System;
using System.Drawing;

namespace Day13
{
    public class Cart
    {
        public Point Position { get; private set; }
        private int _directionX;
        private int _directionY;

        public char Symbol
        {
            get
            {
                if (_directionX > 0) return '>';
                if (_directionX < 0) return '<';
                if (_directionY > 0) return 'v';
                return '^';
            }
        }

        private readonly Action<Cart>[] _actions = 
        {
            TurnLeft,
            DoNothing,
            TurnRight
        };
        private int _currentAction;

        private Cart(Point position, int directionX, int directionY)
        {
            Position = position;
            _directionX = directionX;
            _directionY = directionY;
        }

        public static Cart Create(char c, int positionX, int positionY)
        {
            switch (c)
            {
                case '<':
                    return new Cart(new Point(positionX, positionY), -1, 0);
                case '>':
                    return new Cart(new Point(positionX, positionY), 1, 0);
                case '^':
                    return new Cart(new Point(positionX, positionY), 0, -1);
                case 'v':
                    return new Cart(new Point(positionX, positionY), 0, 1);
                default:
                    throw new ArgumentException($"Unhandled token: {c}", nameof(c));
            }
        }

        public void Update(char[,] tracks)
        {
            MoveForward(this);

            var currentTrack = tracks[Position.X, Position.Y];
            if (currentTrack == '/')
            {
                if (_directionX != 0)
                    TurnLeft(this);
                else
                    TurnRight(this);
            } else if (currentTrack == '\\')
            {
                if (_directionX != 0)
                    TurnRight(this);
                else
                    TurnLeft(this);
            } else if (currentTrack == '+')
            {
                _actions[_currentAction](this);
                _currentAction = (_currentAction + 1) == 3 ? 0 : _currentAction + 1;
            }
        }

        private static void TurnLeft(Cart cart)
        {
            var x = cart._directionY;
            var y = -cart._directionX;

            cart._directionX = x;
            cart._directionY = y;
        }

        private static void TurnRight(Cart cart)
        {
            var x = -cart._directionY;
            var y = cart._directionX;

            cart._directionX = x;
            cart._directionY = y;
        }

        private static void MoveForward(Cart cart)
        {
            cart.Position = new Point(cart.Position.X + cart._directionX, cart.Position.Y + cart._directionY);
        }

        private static void DoNothing(Cart cart)
        {
            
        }
    }
}
