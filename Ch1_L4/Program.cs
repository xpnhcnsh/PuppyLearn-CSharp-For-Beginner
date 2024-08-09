#region 循环/递归/公式
using System.Diagnostics;

var cal = new Calculator();
var sw = new Stopwatch();
int value = 1000;
sw.Start();
Console.WriteLine(cal.SumFrom1ToXLoop(value));
sw.Stop();
Console.WriteLine($"Loop takes {sw.Elapsed.TotalMilliseconds}ms");
sw.Restart();
Console.WriteLine(cal.SumFrom1ToXRecursion(value));
sw.Stop();
Console.WriteLine($"Recursion takes {sw.Elapsed.TotalMilliseconds}ms");
sw.Restart();
Console.WriteLine(cal.SumFrom1ToXMath(value));
sw.Stop();
Console.WriteLine($"Math takes {sw.Elapsed.TotalMilliseconds}ms");
#endregion

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
            int result = x + SumFrom1ToXRecursion(x - 1);
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