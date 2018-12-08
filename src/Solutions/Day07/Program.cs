using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day07
{
    class Program
    {
        static void Main()
        {
            //var input = new[]
            //{
            //    "Step C must be finished before step A can begin.",
            //    "Step C must be finished before step F can begin.",
            //    "Step A must be finished before step B can begin.",
            //    "Step A must be finished before step D can begin.",
            //    "Step B must be finished before step E can begin.",
            //    "Step D must be finished before step E can begin.",
            //    "Step F must be finished before step E can begin.",
            //};
            var input = Input.ReadRows();
            var steps = BuildListOfSteps(input);
            var part1Solution = CalculateOrderOfSteps(steps);
            Console.WriteLine($"Order of steps: {part1Solution}");

            steps = BuildListOfSteps(input);
            int part2Solution = CalculateTimeToComplete(steps);
            Console.WriteLine($"Time to complete: {part2Solution}");

            Console.ReadLine();
        }

        private static List<Step> BuildListOfSteps(string[] input)
        {
            var steps = new List<Step>();
            foreach (var row in input)
            {
                var parts = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var stepName = parts[7][0];
                var dependencyName = parts[1][0];

                var step = steps.FirstOrDefault(s => s.Name == stepName) ?? new Step(stepName);
                var dependency = steps.FirstOrDefault(s => s.Name == dependencyName) ?? new Step(dependencyName);
                step.Dependencies.Add(dependency);
                if (!steps.Contains(dependency))
                    steps.Add(dependency);

                steps.Add(step);
            }

            return steps;
        }

        private static string CalculateOrderOfSteps(List<Step> steps)
        {
            var order = "";
            while (steps.Any(s => !s.Completed))
            {
                var availableSteps = steps.Where(s => s.CanBeCompleted && !s.Completed).ToList();
                var selectedStep = availableSteps.OrderBy(s => s.Name).First();
                selectedStep.Completed = true;
                order += selectedStep.Name;
            }
            return order;
        }

        private static int CalculateTimeToComplete(List<Step> steps)
        {
            var workers = new List<Worker>
            {
                new Worker(),
                new Worker(),
                new Worker(),
                new Worker(),
                new Worker()
            };
            int ticks = 0;
            while (steps.Any(s => !s.Completed))
            {
                var availableWorkers = workers.Where(w => w.Available);
                foreach (var worker in availableWorkers)
                {
                    var availableStep = steps.Where(s => s.CanBeCompleted && !s.Completed && !s.InProgress).OrderBy(s => s.Name).FirstOrDefault();
                    if (availableStep != null)
                        worker.Assign(availableStep);
                }
                foreach (var worker in workers)
                {
                    worker.Work();
                }

                ticks++;
            }

            return ticks;
        }

        private class Step
        {
            public char Name { get; }
            public List<Step> Dependencies { get; } = new List<Step>();
            public bool Completed { get; set; }
            public bool CanBeCompleted => Dependencies.All(d => d.Completed);
            private int Cost { get; }
            private int WorkRemaining { get; set; }
            public bool InProgress { get; set; }

            public Step(char name)
            {
                Name = name;
                Cost = Name - 4;
                WorkRemaining = Cost;
            }

            public void Work()
            {
                WorkRemaining--;
                if (WorkRemaining != 0) return;
                Completed = true;
            }
        }

        private class Worker
        {
            private Step AssignedWork { get; set; }
            public bool Available => AssignedWork == null;
            public void Assign(Step step)
            {
                AssignedWork = step;
                step.InProgress = true;
            }

            public void Work()
            {
                if (AssignedWork == null)
                    return;

                AssignedWork.Work();
                if (AssignedWork.Completed) { 
                    AssignedWork = null;
                }
            }
        }
    }
}
