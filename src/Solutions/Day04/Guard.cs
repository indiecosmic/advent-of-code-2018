using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    class Guard
    {
        public int Id { get; }
        public List<Shift> Shifts { get; }

        public int Sleep => Shifts.Sum(s => s.SleepDuration);
        public Dictionary<int, int> SleepCounts { get; }

        public Guard(IGrouping<int, Shift> grouping)
        {
            Id = grouping.Key;
            Shifts = grouping.ToList();
            SleepCounts = new Dictionary<int, int>();
            var sleepAndWakeupEvents = Shifts.SelectMany(s => s.Events.Where(e => e.EventType == EventType.Sleep || e.EventType == EventType.WakeUp)).ToList();
            for (var min = 0; min < sleepAndWakeupEvents.Count; min++)
            {
                if (sleepAndWakeupEvents[min].EventType == EventType.WakeUp)
                {
                    var sleepStartMinute = sleepAndWakeupEvents[min - 1].DateTime.Minute;
                    var sleepEndMinute = sleepAndWakeupEvents[min].DateTime.Minute;
                    for (var i = sleepStartMinute; i < sleepEndMinute; i++)
                    {
                        if (!SleepCounts.ContainsKey(i)) SleepCounts.Add(i, 0);
                        SleepCounts[i]++;
                    }

                }
            }
        }
    }
}
