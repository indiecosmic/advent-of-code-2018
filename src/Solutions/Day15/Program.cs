using System;
using AdventOfCode.Common;

namespace Day15
{
    class Program
    {
        static void Main()
        {
            var lines = Input.ReadRows();

            var part1Answer = SimulateBattle(lines);
            Console.WriteLine($"Battle outcome: {part1Answer.OutcomeValue}");

            var part2Answer = FindBattleWithoutCasualties(lines);
            Console.WriteLine($"Battle outcome: {part2Answer.OutcomeValue}");

            Console.ReadLine();
        }

        private static BattleOutcome FindBattleWithoutCasualties(string[] lines)
        {
            for (var attackPower = 10;; attackPower++)
            {
                var outcome = SimulateBattle(lines, attackPower);
                if (outcome.Casualties == 0) return outcome;
            }
        }

        private static BattleOutcome SimulateBattle(string[] lines, int elfAttackPower = 3)
        {
            var map = Map.Create(lines, elfAttackPower);
            while (map.Update()) { }
            return new BattleOutcome
            {
                Winner = map.Winner,
                Casualties = map.Casualties,
                OutcomeValue = map.RemainingHitPoints * map.Ticks
            };
        }

        private class BattleOutcome
        {
            public int OutcomeValue { get; set; }
            public int Casualties { get; set; }
            public Pawn.Factions Winner { get; set; }
        }
    }
}
