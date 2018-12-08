using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day04
{
    class Program
    {
        static void Main()
        {
            var input = Input.ReadRows();
            var events = ParseEvents(input);
            var shifts = BuildListOfShifts(events);
            var guards = shifts.GroupBy(s => s.GuardId).Select(g => new Guard(g)).ToList();

            var part1Answer = CalculateStrategy1Answer(guards);
            Console.WriteLine($"Strategy 1 answer: {part1Answer}");

            var part2Answer = CalculateStrategy2Answer(guards);
            Console.WriteLine($"Strategy 2 answer: {part2Answer}");

            Console.ReadLine();
        }

        private static List<Event> ParseEvents(string[] input)
        {
            var events = new List<Event>();
            foreach (var row in input)
            {
                events.Add(new Event(row));
            }
            return events.OrderBy(e => e.DateTime).ToList();
        }

        private static List<Shift> BuildListOfShifts(IEnumerable<Event> events)
        {
            var shifts = new List<Shift>();
            Shift currentShift = new Shift();
            foreach (var evt in events)
            {
                if (evt.EventType == EventType.ShiftStart)
                {
                    currentShift = new Shift(evt);
                    shifts.Add(currentShift);
                }
                else
                {
                    currentShift.AddEvent(evt);
                }
            }
            return shifts;
        }

        private static int CalculateStrategy1Answer(List<Guard> guards)
        {
            var sleepiestGuard = guards.OrderByDescending(g => g.Sleep).First();
            var mostAsleepMinute = sleepiestGuard.SleepCounts.FirstOrDefault(m => m.Value == sleepiestGuard.SleepCounts.Max(n => n.Value));
            return mostAsleepMinute.Key * sleepiestGuard.Id;
        }

        private static int CalculateStrategy2Answer(List<Guard> guards)
        {
            var maxSleepCount = 0;
            Guard selectedGuard = null;
            foreach (var guard in guards)
            {
                if (guard.SleepCounts.Count == 0) continue;
                var guardMax = guard.SleepCounts.Max(sc => sc.Value);
                if (guardMax > maxSleepCount)
                {
                    selectedGuard = guard;
                    maxSleepCount = guardMax;
                }
            }

            if (selectedGuard == null) return 0;
            var minute = selectedGuard.SleepCounts.FirstOrDefault(sc => sc.Value == maxSleepCount);
            return minute.Key * selectedGuard.Id;
        }
    }
}
