namespace Solutions
{
    //Quiz6:
    //给一个List<int>，求前n项和。提示：使用递归。
    public class Sol6
    {
        private dynamic _SumOfFirstNs(List<int> input, int n, out bool flag)
        {
            if (n < 0 || n > input.Count)
            {
                flag = false;
                return "n can neither be negative nor exceed length of input list";
            }
            if (n == 0)
            {
                flag = true;
                return 0;
            }
            else
            {
                flag = true;
                return input[n - 1] + _SumOfFirstNs(input, n - 1, out bool temp);
            }
        }

        public bool SumOfFirstNs(List<int> input, int n, out dynamic res)
        {
            res = _SumOfFirstNs(input, n, out bool flag);
            return flag;
        }
    }
}
