using System.Diagnostics;

#region 求和
var cal = new Calculator();
var sw = new Stopwatch();
ulong value = 10000;
//sw.Start();
//Console.WriteLine(int.MaxValue);
//Console.WriteLine(cal.SumFrom1ToXLoop(value));
//sw.Stop();
//Console.WriteLine($"SumFrom1ToXLoop takes {sw.Elapsed.TotalMilliseconds}ms");
//sw.Restart();
//Console.WriteLine(cal.SumFrom1ToXRecursion(value));
//sw.Stop();
//Console.WriteLine($"SumFrom1ToXRecursion takes {sw.Elapsed.TotalMilliseconds}ms");
//sw.Restart();
//Console.WriteLine(cal.SumFrom1ToXMath(value));
//sw.Stop();
//Console.WriteLine($"SumFrom1ToXMath takes {sw.Elapsed.TotalMilliseconds}ms");
#endregion

#region 阶乘
//value = 10;
//sw.Restart();
//Console.WriteLine(cal.FactorialLoop(value));
//sw.Stop();
//Console.WriteLine($"FactorialLoop takes {sw.Elapsed.TotalMilliseconds}ms");
//sw.Restart();
//Console.WriteLine(cal.FactorialRecursion(value));
//sw.Stop();
//Console.WriteLine($"FactorialRecursion takes {sw.Elapsed.TotalMilliseconds}ms");
//sw.Restart();
#endregion

#region 二分查找
//sw.Restart();
//Console.WriteLine(cal.BinarySearch([1, 2, 4, 3, 6, 5, 0], 1));
//sw.Stop();
//Console.WriteLine($"BinarySearch takes {sw.Elapsed.TotalMilliseconds}ms");
#endregion

#region 判断List中元素是否唯一
Console.WriteLine(cal.CheckUnique([1, 2, 4, 3, 6, 1, 5, 0]));
#endregion

class Calculator
{
    /// <summary>
    /// O(n) 时间复杂度
    /// </summary>
    public ulong SumFrom1ToXLoop(ulong x)
    {
        ulong result = 0;
        for (ulong i = 0; i <= x; i++)
        {
            result += i;
        }
        return result;
    }

    /// <summary>
    /// O(1)<T<O(n),但更占用内存。
    /// </summary>
    public ulong SumFrom1ToXRecursion(ulong x)
    {
        if (x == 1)
        {
            return 1;
        }
        else
        {
            ulong result = x + SumFrom1ToXRecursion(x - 1);
            return result;
        }
    }

    /// <summary>
    /// O(1)
    /// </summary>
    public ulong SumFrom1ToXMath(ulong x)
    {
        return (1 + x) * x / 2;
    }

    /// <summary>
    /// O(n)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public ulong FactorialLoop(ulong x)
    {
        ulong result = 1;
        for (ulong i = 1; i <= x; i++)
        {
            result *= i;
        }
        return result;
    }

    /// <summary>
    /// O(n)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public ulong FactorialRecursion(ulong x)
    {
        if (x == 1)
            return 1;
        return x * FactorialRecursion(x - 1);
    }

    /// <summary>
    /// O(logn)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="target"></param>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    private (bool, string) _BinarySearch(List<int> x, int target, int low, int high)
    {
        if (x.Count == 0)
        {
            return (false, "The length of input list can't be 0!");
        }

        if (low > high)
            return (false, "Target not found!");
        else
        {
            int mid = (low + high) / 2;
            if (x[mid] == target)
                return (true, "Target found!");
            else if (x[mid] < target)
                return _BinarySearch(x, target, mid + 1, high);
            else
                return _BinarySearch(x, target, low, mid - 1);
        }
    }

    public string BinarySearch(List<int> x, int target)
    {
        var temp = x.OrderBy(x => x).ToList();
        int low = 0;
        int high = x.Count - 1;
        var res = _BinarySearch(temp, target, low, high);
        switch (res.Item1)
        {
            case true:
                return $"{res.Item2} The index is {x.IndexOf(target)}";
            case false:
                return res.Item2;
        }
    }

    public string CheckUnique(List<int> x)
    {
        var sw = new Stopwatch();
        var low = 0; ;
        var high = x.Count - 1;
        sw.Start();
        var ResRecursion = _CheckUniqueRecursion(x, low, high);
        sw.Stop();
        var t1 = sw.Elapsed.TotalMilliseconds;
        sw.Restart();
        var Res2Loop = _CheckUnique2Loop(x);
        sw.Stop();
        var t2 = sw.Elapsed.TotalMilliseconds;
        sw.Restart();
        var ResSorted = _CheckUniqueSorted(x);
        sw.Stop();
        var t3 = sw.Elapsed.TotalMilliseconds;
        sw.Restart();
        var ResDistinct = _CheckUniqueDistinct(x);
        sw.Stop();
        var t4 = sw.Elapsed.TotalMilliseconds;
        sw.Restart();
        var ResHashSet = _CheckUniqueHashSet(x);
        sw.Stop();
        var t5 = sw.Elapsed.TotalMilliseconds;
        return $"ResRecursion:{ResRecursion},{t1}; Res2Loop:{Res2Loop},{t2}; ResSorted:{ResSorted},{t3}; ResDistinct:{ResDistinct},{t4}; ResHashSet:{ResHashSet},{t5};";
    }

    /// <summary>
    /// Worst practice, O(2^n)
    /// </summary>
    /// <param name="x">Input List&lt;int&gt;</param>
    /// <param name="low">Lowest index</param>
    /// <param name="high">Highest index</param>
    /// <returns></returns>
    private bool _CheckUniqueRecursion(List<int> x, int low, int high)
    {
        if (high - low < 1)
            return true;
        else if (!_CheckUniqueRecursion(x, low, high - 1))
            return false;
        else if (!_CheckUniqueRecursion(x, low + 1, high))
            return false;
        else
            return x[low] != x[high];
    }

    /// <summary>
    /// Normal one, O(n^2)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private bool _CheckUnique2Loop(List<int> x)
    {
        for (int i = 0; i < x.Count; i++)
        {
            for (int j = i + 1; j < x.Count; j++)
            {
                if (x[i] == x[j])
                    return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Better practice, O(nlogn)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private bool _CheckUniqueSorted(List<int> x) 
    {
        x.Sort(); //O(nlogn)
        for (int i = 0; i < x.Count-1; i++) //O(n-1)
        {
            if (x[i] == x[i + 1])
                return false;
        }
        return true; //O(nlogn)
    }

    private bool _CheckUniqueDistinct(List<int> x)
    {
        return x.Count == x.Distinct().Count();
    }

    /// <summary>
    /// Best practice, O(1)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private bool _CheckUniqueHashSet(List<int> x)
    {
        return new HashSet<int>(x).Count == x.Count;
    }
}