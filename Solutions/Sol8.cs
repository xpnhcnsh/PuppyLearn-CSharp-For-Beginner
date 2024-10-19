using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions
{
    public class Sol8
    {
        static public ref int FindMax(int[] input)
        {
            int idx = Array.FindIndex(input, (int x) => x == input.Max());
            return ref input[idx];
        }
    }
}
