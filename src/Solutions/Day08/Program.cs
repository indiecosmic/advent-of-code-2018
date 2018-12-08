using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day08
{
    class Program
    {
        static void Main()
        {
            //var input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
            var input = Input.ReadInput();
            var root = CreateTree(input);

            var part1Solution = root.MetadataSum();
            Console.WriteLine($"Sum of metadata entries: {part1Solution}");

            var part2Solution = root.GetValue();
            Console.WriteLine($"Value of root node: {part2Solution}");

            Console.ReadLine();
        }

        private static Node CreateTree(string input)
        {
            var numbers = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var position = 0;
            var root = CreateNode(numbers, ref position);
            return root;
        }

        private static Node CreateNode(int[] numbers, ref int position)
        {
            var node = new Node(numbers[position++], numbers[position++]);
            for (var i = 0; i < node.ChildCount; i++)
            {
                var child = CreateNode(numbers, ref position);
                node.ChildNodes.Add(child);
            }
            for (var i = 0; i < node.MetadataCount; i++)
            {
                node.MetadataEntries.Add(numbers[position++]);
            }
            return node;
        }

        private class Node
        {
            public int ChildCount { get; }
            public int MetadataCount { get; }
            public List<Node> ChildNodes { get; } = new List<Node>();
            public List<int> MetadataEntries { get; } = new List<int>();
            public Node(int childCount, int metadataCount)
            {
                ChildCount = childCount;
                MetadataCount = metadataCount;
            }

            public int MetadataSum()
            {
                var ownSum = MetadataEntries.Sum() + ChildNodes.Sum(n => n.MetadataSum());
                return ownSum;
            }

            public int GetValue()
            {
                if (ChildCount == 0) return MetadataEntries.Sum();
                var value = 0;
                foreach (var metadataEntry in MetadataEntries)
                {
                    if (metadataEntry == 0) continue;
                    var index = metadataEntry - 1;
                    if (index > ChildCount - 1) continue;
                    value += ChildNodes[index].GetValue();
                }
                return value;
            }
        }
    }
}
