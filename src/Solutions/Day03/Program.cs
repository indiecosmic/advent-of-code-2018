using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Common;

namespace Day03
{
    static class Program
    {
        private static void Main()
        {
            var rows = Input.ReadRows();

            var claims = ParseClaims(rows);
            var part1Solution = CalculateNumberOfClaims(claims);
            Console.WriteLine($"Number of square inches: {part1Solution}");

            var part2Solution = FindNonOverlappingClaim(claims);
            Console.WriteLine($"Claim not overlapping: {part2Solution.Id}");

            Console.ReadLine();
        }

        private static List<Claim> ParseClaims(IEnumerable<string> rows)
        {
            return rows.Select(row => new Claim(row)).ToList();
        }

        private static Claim FindNonOverlappingClaim(List<Claim> claims)
        {
            foreach (var claim in claims)
            {
                var otherClaims = claims.Where(c => c.Id != claim.Id);
                var count = otherClaims.Count(oc => oc.Overlaps(claim));
                if (count == 0)
                    return claim;
            }

            return null;
        }


        private static int CalculateNumberOfClaims(List<Claim> claims)
        {
            var width = claims.Max(c => c.Left + c.Width);
            var height = claims.Max(c => c.Top + c.Height);
            var result = 0;

            for (var x = 1; x < width; x++)
            {
                for (var y = 1; y < height; y++)
                {
                    var claimsCount = claims.Count(c => c.Contains(x, y));
                    if (claimsCount > 1) result++;
                }
            }

            return result;
        }

    }
}
