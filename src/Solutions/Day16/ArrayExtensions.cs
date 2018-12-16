using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day16
{
    public static class ArrayExtensions
    {
        public static bool SameAs(this int[] array, int[] other)
        {
            if (array.Length != other.Length)
                return false;
            return !array.Where((t, i) => t != other[i]).Any();
        }
    }
}
