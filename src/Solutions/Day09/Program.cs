using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day09
{
    class Program
    {
        static void Main()
        {
            var input = Input.ReadInput();
            var parts = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var numberOfPlayers = int.Parse(parts[0]);
            var rounds = int.Parse(parts[6]);
            var multiplier = 1;

            var part1Solution = CalculateHighScore(numberOfPlayers, rounds, multiplier);
            Console.WriteLine($"Players: {numberOfPlayers}, rounds: {rounds * multiplier}, high score: {part1Solution}");

            multiplier = 100;
            var part2Solution = CalculateHighScore(numberOfPlayers, rounds, multiplier);
            Console.WriteLine($"Players: {numberOfPlayers}, rounds: {rounds * multiplier}, high score: {part2Solution}");

            Console.ReadLine();
        }

        private static int CalculateHighScore(int numberOfPlayers, int lastMarbleWorth, int multiplier = 1)
        {
            var rounds = lastMarbleWorth * multiplier;

            var game = new MarbleGame();
            var players = CreatePlayers(numberOfPlayers);
            var currentPlayer = 0;
            for (var i = 1; i <= rounds; i++)
            {
                game.PlaceMarble(players[currentPlayer], i);
                currentPlayer++;
                if (currentPlayer >= players.Length)
                    currentPlayer = 0;
            }

            return players.Max(p => p.Score);
        }

        private static Player[] CreatePlayers(int count)
        {
            var players = new List<Player>();
            for (var i = 0; i < count; i++)
                players.Add(new Player());

            return players.ToArray();
        }
    }
}
