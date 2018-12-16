using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main()
        {
            var recipes = new LinkedList<int>(new[] { 3, 7 });
            var elf1 = recipes.First;
            var elf2 = recipes.Last;
            var input = 110201;
            var count = 10;

            while (recipes.Count < input + count)
            {
                var newRecipe = elf1.Value + elf2.Value;
                if (newRecipe >= 10)
                    recipes.AddLast(1);
                recipes.AddLast(newRecipe % 10);

                var elf1Steps = 1 + elf1.Value;
                for (var elf1Moves = 0; elf1Moves < elf1Steps; elf1Moves++)
                {
                    elf1 = elf1.Next ?? recipes.First;
                }
                var elf2Steps = 1 + elf2.Value;
                for (var elf2Moves = 0; elf2Moves < elf2Steps; elf2Moves++)
                {
                    elf2 = elf2.Next ?? recipes.First;
                }
            }

            var timer = Stopwatch.StartNew();
            var part1Answer = CalculateScore(recipes, input, count);
            timer.Stop();

            var timer2 = Stopwatch.StartNew();
            var part2Answer = CalculateScoreBackwards("110201");
            timer2.Stop();


            Console.WriteLine($"Score: {part1Answer} ({timer.ElapsedMilliseconds})");
            Console.WriteLine($"Number of recipes: {part2Answer} ({timer2.ElapsedMilliseconds})");
            Console.ReadLine();
        }

        private static int CalculateScoreBackwards(string input)
        {
            var numbersToCheck = input.Select(s => (int)char.GetNumericValue(s)).ToArray();
            int index = 0;
            int positionToCheck = 0;
            bool notFound = true;
            List<int> numbers = new List<int> { 3, 7 };
            int currentRecipe1 = 0;
            int currentRecipe2 = 1;
            while (notFound)
            {
                int recipe1 = numbers[currentRecipe1];
                int recipe2 = numbers[currentRecipe2];
                int sum = recipe1 + recipe2;
                if (sum < 10)
                {
                    numbers.Add(sum);
                }
                else
                {
                    numbers.Add(1);
                    numbers.Add(sum - 10);
                }

                currentRecipe1 = (currentRecipe1 + 1 + recipe1) % numbers.Count;
                currentRecipe2 = (currentRecipe2 + 1 + recipe2) % numbers.Count;

                while (index + positionToCheck < numbers.Count)
                {
                    if (numbersToCheck[positionToCheck] == numbers[index + positionToCheck])
                    {
                        if (positionToCheck == numbersToCheck.Length - 1)
                        {
                            notFound = false;
                            break;
                        }
                        positionToCheck++;
                    }
                    else
                    {
                        positionToCheck = 0;
                        index++;
                    }
                }
            }
            return index;
        }


        private static string CalculateScore(LinkedList<int> recipes, int input, int count)
        {
            var current = recipes.First;
            for (var i = 0; i < input; i++)
            {
                current = current.Next ?? recipes.First;
            }

            var score = "";
            for (var i = 0; i < count; i++)
            {
                score += current.Value;
                current = current.Next ?? recipes.First;
            }

            return score;
        }
    }
}
