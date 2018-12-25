using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day24
{
    class Group
    {
        public int GroupNumber { get; }
        public int Units { get; private set; }
        public int AttackDamage { get; private set; }
        public int Initiative { get; }
        public int HitPoints { get; }
        public string AttackType { get; }
        public List<string> Immunities { get; }
        public List<string> Weaknesses { get; }
        public int EffectivePower => Units * AttackDamage;
        public Faction Faction { get; }
        public bool CanAttack => Units > 0;

        private Group(int groupNumber, int units, int attackDamage, int initiative, int hitPoints, string attackType,
            Faction faction, List<string> immunities, List<string> weaknesses)
        {
            GroupNumber = groupNumber;
            Units = units;
            AttackDamage = attackDamage;
            Initiative = initiative;
            HitPoints = hitPoints;
            AttackType = attackType;
            Faction = faction;
            Immunities = immunities;
            Weaknesses = weaknesses;
        }

        public int PotentialDamage(Group attacker)
        {
            if (Immunities.Contains(attacker.AttackType)) return 0;
            var multiplier = Weaknesses.Contains(attacker.AttackType) ? 2 : 1;
            var damage = multiplier * attacker.EffectivePower;

            return damage;
        }

        public Group SelectTarget(IEnumerable<Group> potentialTargets)
        {
            return potentialTargets.OrderByDescending(g => (g.PotentialDamage(this), g.EffectivePower, g.Initiative))
                .FirstOrDefault();
        }

        public int Attack(Group target)
        {
            var originalUnits = target.Units;
            var damage = target.PotentialDamage(this);
            target.DealDamage(damage);
            //if (Faction == Faction.ImmuneSystem)
            //    Console.ForegroundColor = ConsoleColor.Green;
            //else
            //    Console.ForegroundColor = ConsoleColor.Red;

            //Console.WriteLine($"{Faction} group {GroupNumber} attacks {target.Faction} group {target.GroupNumber} with {damage} damage, killing {originalUnits - target.Units} units");
            //Console.ForegroundColor = ConsoleColor.White;
            return originalUnits - target.Units;
        }

        private void DealDamage(int damage)
        {
            var unitsLost = damage / HitPoints;
            Units -= unitsLost;
            if (Units < 0) Units = 0;
        }

        public void Boost(int value)
        {
            AttackDamage += value;
        }

        public override string ToString()
        {
            return
                $"{Units} units each with {HitPoints} hit points(weak to {string.Join(",", Weaknesses)}; immune to {string.Join(",", Immunities)}) with an attack that does {AttackDamage} {AttackType} damage at initiative {Initiative}";
        }

        public static Group Parse(string input, Faction faction, int groupNumber)
        {
            var matches = Regex.Matches(input,
                @"(?<units>\d+) units|(?<hp>\d+) hit points|weak to (?<weak>[\w,\s]+)|immune to (?<immune>[\w,\s]+)|initiative (?<initiative>\d+)|does (?<dmg>\d+)|(?<type>\b\w+\b) damage");
            if (matches.Count == 0) throw new ArgumentException($"Invalid input: {input}", nameof(input));
            var immunities = "";
            var weaknesses = "";
            var attackDamage = 0;
            var hitPoints = 0;
            var initiative = 0;
            var attackType = "";
            var units = 0;
            foreach (Match match in matches)
            {
                if (match.Groups["dmg"].Success)
                    attackDamage = int.Parse(match.Groups["dmg"].Value);
                else if (match.Groups["hp"].Success)
                    hitPoints = int.Parse(match.Groups["hp"].Value);
                else if (match.Groups["immune"].Success)
                    immunities = match.Groups["immune"].Value;
                else if (match.Groups["weak"].Success)
                    weaknesses = match.Groups["weak"].Value;
                else if (match.Groups["initiative"].Success)
                    initiative = int.Parse(match.Groups["initiative"].Value);
                else if (match.Groups["type"].Success)
                    attackType = match.Groups["type"].Value;
                else if (match.Groups["units"].Success)
                    units = int.Parse(match.Groups["units"].Value);
            }

            var weaknessesList = new List<string>();
            if (!string.IsNullOrEmpty(weaknesses))
            {
                weaknessesList.AddRange(weaknesses.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }
            var immunitiesList = new List<string>();
            if (!string.IsNullOrEmpty(immunities))
            {
                immunitiesList.AddRange(immunities.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }

            return new Group(groupNumber, units, attackDamage, initiative, hitPoints, attackType, faction, immunitiesList, weaknessesList);
        }
    }
    public enum Faction
    {
        None,
        ImmuneSystem,
        Infection
    }
}
