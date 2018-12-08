using System;

namespace Day04
{
    class Event
    {
        public DateTime DateTime { get; }
        public string Description { get; }
        public EventType EventType { get; }

        public Event(string input)
        {
            var parts = input.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries);
            DateTime = DateTime.Parse(parts[0]);
            Description = parts[1].Trim();
            if (Description.Contains("wakes up")) EventType = EventType.WakeUp;
            else if (Description.Contains("falls asleep")) EventType = EventType.Sleep;
            else
            {
                EventType = EventType.ShiftStart;
            }
        }
    }

    enum EventType
    {
        ShiftStart,
        Sleep,
        WakeUp
    }
}
