using System.Collections.Generic;

namespace Day09
{
    class MarbleGame
    {
        private readonly LinkedList<int> _marbles = new LinkedList<int>();
        private LinkedListNode<int> _currentNode;

        public MarbleGame()
        {
            _currentNode = _marbles.AddFirst(0);
        }

        public void PlaceMarble(Player player, int number)
        {
            if (number % 23 == 0)
            {
                for (var i = 0; i < 7; i++)
                {
                    _currentNode = _currentNode.Previous ?? _marbles.Last;
                }
                player.Score += number;
                player.Score += _currentNode.Value;
                var remove = _currentNode;
                _currentNode = _currentNode.Next ?? _marbles.First;
                _marbles.Remove(remove);
                return;
            }
            _currentNode = _currentNode.Next ?? _marbles.First;
            var newValue = new LinkedListNode<int>(number);
            _marbles.AddAfter(_currentNode, newValue);
            _currentNode = newValue;
        }
    }
}
