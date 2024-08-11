namespace Solutions
{
    public static class Sol3
    {
        public static void Run()
        {
            while (true)
            {
                Console.WriteLine("\t\nPlease input some numbers, separated by English comma. Type in 'exit' to close.");
                try
                {
                    List<string> list = new List<string>();
                    string? input = Console.ReadLine();
                    // edge cases
                    if (string.IsNullOrEmpty(input)) //input=""
                        throw new Exception("输入不能为空");
                    else if (input.Trim() == "") //"" = "     ".Trim()
                        throw new Exception("输入不能为空");
                    else if (input.Contains("，"))
                        throw new Exception("逗号必须是英文字符");
                    if (input.Equals("exit"))
                        Environment.Exit(0);
                    IEnumerable<double> inputNum = input.Split(",").Select(x => Double.Parse(x));
                    //IEnumerable<double> inputNum = input.Split(',').Select(x =>
                    //{
                    //    double temp;
                    //    if (!Double.TryParse(x, out temp))
                    //        throw new Exception($"{x} is not a valid number!");
                    //    return temp;
                    //});
                    Console.WriteLine($"The max is: {inputNum.Max()};The minum is: {inputNum.Min()};The average is: {inputNum.Average()}");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }
    }
}
