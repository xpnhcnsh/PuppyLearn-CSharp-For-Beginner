namespace Ch1_L3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 循环/递归/公式
            var cal = new Calculator();
            Console.WriteLine(cal.SumFrom1ToXLoop(10));
            Console.WriteLine(cal.SumFrom1ToXRecursion(10));
            Console.WriteLine(cal.SumFrom1ToXMath(10));
            #endregion
        }

        
        class Calculator
        {
            /// <summary>
            /// O(n)
            /// </summary>
            public int SumFrom1ToXLoop(int x)
            {
                int result = 0;
                for (int i = 0; i <= x; i++)
                {
                    result += i;
                }
                return result;
            }

            /// <summary>
            /// O(1)<T<O(n),但更占用内存。
            /// </summary>
            public int SumFrom1ToXRecursion(int x)
            {
                if (x == 1)
                {
                    return 1;
                }
                else
                {
                    int result = x + SumFrom1ToXRecursion(x-1);
                    return result;
                }
            }

            /// <summary>
            /// O(1)
            /// </summary>
            public int SumFrom1ToXMath(int x)
            {
                return (1 + x) * x / 2;
            }
        }
    }
}
