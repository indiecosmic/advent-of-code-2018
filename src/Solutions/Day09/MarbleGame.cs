using System;
using System.Collections.Generic;
using System.Text;

namespace Day09
{
    class MarbleGame
    {
        private readonly List<int> _marbles = new List<int>();
        //private readonly LinkedList<int> _marbles2 = new LinkedList<int>();
        //private LinkedListNode<int> _currentNode;
        private int _current;

        public MarbleGame()
        {
            _marbles.Add(0);
            //_currentNode = _marbles2.AddFirst(0);
            _current = 0;
        }

        public void PlaceMarble(Player player, int number)
        {
            var length = _marbles.Count;
            var current = _current;

            if (number % 23 == 0)
            {
                current -= 7;
                if (current < 0)
                    current += length;

                player.Score += number;
                player.Score += _marbles[current];

                _marbles.RemoveAt(current);
                _current = current;
                return;
            }

            current += 2;
            if (current > length)
                current -= length;

            _marbles.Insert(current, number);
            _current = current;
        }
    }
}
