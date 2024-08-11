using System.Data;
using System.Linq.Expressions;

namespace Solutions
{
    public static class Sol4
    {
        /// <summary>
        /// 使用Expression tree实现
        /// </summary>
        public static void RunV1()
        {
            var rnd = new Random(new Guid().GetHashCode());
            while (true)
            {
                int min = -100;
                int max = 101;
                try
                {
                    int life = 3;
                    int left = rnd.Next(min, max);
                    Expression leftExp = Expression.Constant((double)left);
                    int right = rnd.Next(min, max);
                    Expression rightExp = Expression.Constant((double)right);
                    int op = rnd.Next(1, 5);
                    Expression expression = op switch
                    {
                        1 => Expression.AddChecked(leftExp, rightExp),
                        2 => Expression.SubtractChecked(leftExp, rightExp),
                        3 => Expression.MultiplyChecked(leftExp, rightExp),
                        4 => Expression.Divide(leftExp, rightExp),
                        _ => throw new Exception("No operator matched!"),
                    };
                    double groundTrue = Math.Round(Expression.Lambda<Func<double>>(expression).Compile()(), 2);
                    //Console.WriteLine($"{expression.ToString()} = ?");
                    string expressionStr = (op, left, right) switch
                    {
                        { op: 1, right: > 0 } => $"{left} + {right} = ?",
                        { op: 1, right: < 0 } => $"{left} - {-right} = ?",
                        { op: 2, right: > 0 } => $"{left} - {right} = ?",
                        { op: 2, right: < 0 } => $"{left} + {-right} = ?",
                        { op: 3, right: > 0 } => $"{left} * {right} = ?",
                        { op: 3, right: < 0 } => $"{-left} * {-right} = ?",
                        { op: 4, right: > 0 } => $"{left} / {right} = ?",
                        { op: 4, right: < 0 } => $"{-left} / {-right} = ?",
                    };
                    Console.WriteLine(expressionStr);
                    while (life > 0)
                    {
                        Console.WriteLine($"Type in a result, round to 2 decimal places; have {life} chances in total...(type in 'exit' to quit)");
                        string? input = Console.ReadLine();
                        double inputNum;
                        if (string.IsNullOrEmpty(input))
                        {
                            life -= 1;
                            Console.WriteLine($"Input can not be empty, have {life} chances left...");
                        }
                        else if (input.Equals("exit"))
                            Environment.Exit(0);
                        else if (Double.TryParse(input, out inputNum))
                        {
                            if (inputNum == groundTrue)
                            {
                                Console.WriteLine("Congrats!\t\n");
                                life = -1;
                            }
                            else
                            {
                                {
                                    life -= 1;
                                    Console.WriteLine($"Wrong answer，have {life} chances left...");
                                }
                            }
                        }
                        else
                        {
                            life -= 1;
                            Console.WriteLine($"Invalid input! Have {life} chances left...");
                        }
                        if (life == 0)
                            Console.WriteLine($"Run out of chances! The right answer is {groundTrue}! Learn some math!\t\n");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// 使用DataTable实现
        /// </summary>
        public static void RunV2()
        {
            while (true)
            {
                DataTable dt = new DataTable();
                var rnd = new Random(new Guid().GetHashCode());
                while (true)
                {
                    int min = -100;
                    int max = 101;
                    try
                    {
                        int life = 3;
                        int left = rnd.Next(min, max);
                        string leftStr = left.ToString();
                        int right = rnd.Next(min, max);
                        string rightStr = right.ToString();
                        int opInt = rnd.Next(1, 5);
                        string op = opInt switch
                        {
                            1 => "+",
                            2 => "-",
                            3 => "*",
                            4 => "/",
                            _ => throw new Exception("No operator matched!"),
                        };
                        string expression = $"{left} {op} {right}";
                        var groundTrueStr = dt.Compute(expression, null).ToString();
                        var groundTrue = Math.Round(Double.Parse(groundTrueStr), 2);
                        //Console.WriteLine($"{expression.ToString()} = ?");
                        string expressionStr = (opInt, left, right) switch
                        {
                            { opInt: 1, right: > 0 } => $"{left} + {right} = ?",
                            { opInt: 1, right: < 0 } => $"{left} - {-right} = ?",
                            { opInt: 2, right: > 0 } => $"{left} - {right} = ?",
                            { opInt: 2, right: < 0 } => $"{left} + {-right} = ?",
                            { opInt: 3, right: > 0 } => $"{left} * {right} = ?",
                            { opInt: 3, right: < 0 } => $"{-left} * {-right} = ?",
                            { opInt: 4, right: > 0 } => $"{left} / {right} = ?",
                            { opInt: 4, right: < 0 } => $"{-left} / {-right} = ?",
                        };
                        Console.WriteLine(expressionStr);
                        while (life > 0)
                        {
                            Console.WriteLine($"Type in a result, round to 2 decimal places; have {life} chances in total...(type in 'exit' to quit)");
                            string? input = Console.ReadLine();
                            double inputNum;
                            if (string.IsNullOrEmpty(input))
                            {
                                life -= 1;
                                Console.WriteLine($"Input can not be empty, have {life} chances left...");
                            }
                            else if (input.Equals("exit"))
                                Environment.Exit(0);
                            else if (Double.TryParse(input, out inputNum))
                            {
                                if (inputNum == groundTrue)
                                {
                                    Console.WriteLine("Congrats!\t\n");
                                    life = -1;
                                }
                                else
                                {
                                    {
                                        life -= 1;
                                        Console.WriteLine($"Wrong answer，have {life} chances left...");
                                    }
                                }
                            }
                            else
                            {
                                life -= 1;
                                Console.WriteLine($"Invalid input! Have {life} chances left...");
                            }
                            if (life == 0)
                                Console.WriteLine($"Run out of chances! The right answer is {groundTrue}! Learn some math!\t\n");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
