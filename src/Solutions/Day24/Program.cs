using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day24
{
    class Program
    {
        static void Main()
        {
            var input = Input.ReadRows();
            
            var factions = CreateFactions(input);
            var part1Answer = SimulateBattle(factions.immuneSystem, factions.infection);
            Console.WriteLine($"{part1Answer.winner} wins. Total remaining units {part1Answer.unitsRemaining}");

            var part2Answer = SimulateBattleWithBoost(input);
            Console.WriteLine($"{part2Answer.winner} wins. Total remaining units {part2Answer.unitsRemaining}");

            Console.ReadLine();
        }

        private static (Faction winner, int unitsRemaining) SimulateBattleWithBoost(string[] input)
        {
            var minBoost = 0;
            var maxBoost = 5000;
            (Faction winner, int unitsRemaining) outcome;
            while (maxBoost - minBoost > 1)
            {
                var boost = (maxBoost + minBoost) / 2;
                var factions = CreateFactions(input);
                foreach (var group in factions.immuneSystem)
                    group.Boost(boost);

                outcome = SimulateBattle(factions.immuneSystem, factions.infection);
                if (outcome.winner == Faction.ImmuneSystem)
                {
                    maxBoost = boost;
                }
                else
                {
                    minBoost = boost;
                }
            }

            var lastRound = CreateFactions(input);
            foreach (var group in lastRound.immuneSystem)
                group.Boost(maxBoost);

            outcome = SimulateBattle(lastRound.immuneSystem, lastRound.infection);

            return outcome;
        }

        private static (Faction winner, int unitsRemaining) SimulateBattle(List<Group> immuneSystem, List<Group> infection)
        {
            var attackOrder = new PriorityQueue<(Group attacker, Group target)>();
            var targets = new HashSet<Group>();
            while (immuneSystem.Count > 0 && infection.Count > 0)
            {
                // Target selection
                var selectionOrder = immuneSystem.Union(infection).OrderByDescending(g => (g.EffectivePower, g.Initiative)).ToList();
                foreach (var attacker in selectionOrder)
                {
                    var target = attacker.Faction == Faction.ImmuneSystem
                        ? attacker.SelectTarget(infection.Where(g => !targets.Contains(g)))
                        : attacker.SelectTarget(immuneSystem.Where(g => !targets.Contains(g)));
                    if (target != null && target.PotentialDamage(attacker) > 0)
                    {
                        targets.Add(target);
                        attackOrder.Enqueue((attacker, target), attacker.Initiative);
                    }
                }


                var stalemateCount = 0;
                var attackCount = attackOrder.Count;
                // Attack phase
                while (attackOrder.TryDequeueMax(out var battle))
                {
                    if (battle.attacker.CanAttack) { 
                        var damageDealt = battle.attacker.Attack(battle.target);
                        if (damageDealt == 0) stalemateCount++;
                    }
                }

                if (stalemateCount == attackCount)
                {
                    return (Faction.None, 0);
                }

                immuneSystem = immuneSystem.Where(i => i.Units > 0).ToList();
                infection = infection.Where(i => i.Units > 0).ToList();
                targets.Clear();
            }

            var winner = immuneSystem.Count > 0 ? immuneSystem : infection;
            var totalUnits = winner.Sum(w => w.Units);
            var winningFaction = winner.First().Faction;
            return (winningFaction, totalUnits);
        }

        static (List<Group> immuneSystem, List<Group> infection) CreateFactions(string[] input)
        {
            var immuneSystem = new List<Group>();
            var infection = new List<Group>();
            var currentGroup = immuneSystem;
            var currentFaction = Faction.ImmuneSystem;
            var groupNumber = 0;
            foreach (var line in input)
            {
                if (line.StartsWith("Immune"))
                {
                    currentGroup = immuneSystem;
                    currentFaction = Faction.ImmuneSystem;
                    groupNumber = 0;
                }
                else if (line.StartsWith("Infection"))
                {
                    currentGroup = infection;
                    currentFaction = Faction.Infection;
                    groupNumber = 0;
                }
                else
                {
                    currentGroup.Add(Group.Parse(line, currentFaction, groupNumber));
                }

                groupNumber++;
            }

            return (immuneSystem, infection);
        }
    }
}
