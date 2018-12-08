using System;
using System.Collections.Generic;

namespace Day04
{
    class Shift
    {
        public int GuardId { get; }

        public List<Event> Events { get; }

        public int SleepDuration { get; private set; }


        public Shift()
        {
            Events = new List<Event>();
            SleepDuration = 0;
        }

        public Shift(Event evt)
            : this()
        {
            Events.Add(evt);
            var idStart = evt.Description.IndexOf("#", StringComparison.Ordinal);
            var idEnd = evt.Description.IndexOf(' ', idStart);
            var id = evt.Description.Substring(idStart + 1, idEnd - idStart);
            GuardId = int.Parse(id);
        }

        public void AddEvent(Event evt)
        {
            if (evt.EventType == EventType.WakeUp)
            {
                var sleepStart = Events[Events.Count - 1].DateTime;
                var sleepEnd = evt.DateTime;
                var sleepDuration = sleepEnd - sleepStart;
                SleepDuration += (int) sleepDuration.TotalMinutes;
            }

            Events.Add(evt);
        }
    }
}
