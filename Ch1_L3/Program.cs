using System.Linq.Expressions;
using System.Data;

namespace Ch1_L3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 模式匹配
            //类型模式
            //var x = new int[] { 1, 2, 3, 4 }; // array数组，数组中每个item都是int类型。
            //Console.WriteLine(GetSourceLabel(x));
            //var y = new List<char> { 'a', 'b', 'c' };
            //Console.WriteLine(GetSourceLabel(y));

            //var v1 = Vehicle.Car;
            //var v2 = Vehicle.Train;
            //Console.WriteLine(GetToll(v1));
            //Console.WriteLine(GetToll(v2));

            //逻辑模式
            //Console.WriteLine(GetSeason(new DateTime(2021, 1, 19)));

            //列表模式
            //int[] x = { 1, 2, 3, 4, 10, 6, 0 };
            //int[] y = { 1, 2, 3, 4, 3, 10, 22, 123 };
            //Console.WriteLine(x is [1, 2, 3]);
            //Console.WriteLine(y is [1 or 2, < 3, int, <= 4, 3, ..]); //..表示不对后续内容进行匹配，后续元素可以为null; _表示匹配除null外的任意元素。

            //使用..对list进行切片，使用var设置一个临时变量，可在模式匹配语句内部使用，一个模式匹配中只能有一个..切片
            //void MatchMsg(string msg)
            //{
            //    var res = msg is [>= 'a' and <= 'z' or >= 'A' and <= 'Z', .. var inner, >= 'a' and <= 'z' or >= 'A' and <= 'Z']
            //    ? $"Message {msg} matches; inner part is {inner}."
            //    : $"Message {msg} doesn't match.";
            //    Console.WriteLine(res);
            //}
            //MatchMsg("a123d");
            //MatchMsg("1234d");

            //void Validate(int[] value)
            //{
            //    var res = value is [_, .. { Length: 3 }, > 0] ? "valid" : "not valid";
            //    Console.WriteLine(res);
            //}
            //Validate([-10, 1, 2, 100, 10]);
            #endregion

            #region Checked Unchecked
            ////uint 32位无符号数
            //uint x = uint.MaxValue;
            ////Console.WriteLine($"x:{x}, binary:{Convert.ToString(x, 2)}");
            //// unit.MaxValue + 1后发生溢出，变成0。因为加1后，二进制从低位开始进位，全部变成0，最高位进位后超出32位限制，被舍弃，因此变成32位0。
            ////uint y = x + 1;
            ////Console.WriteLine($"y:{y}");
            //// 使用checked对OverflowException进行捕捉，如果不用try catch，程序会崩溃。
            //// 使用unchecked则不会捕捉溢出Exception，默认使用unchecked。
            //try
            //{
            //    uint z = checked(x + 1);
            //}
            //catch (OverflowException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //// checked/unchecked还有一种上下文用法：
            //checked
            //{
            //    try
            //    {
            //        uint w = x + 1;
            //    }
            //    catch (Exception ex)
            //    {

            //        Console.WriteLine(ex.Message);
            //    }
            //}
            #endregion

            #region ->指针操作符
            // 要使用指针，必须在"项目-》项目属性-》生成-》常规中，勾选‘允许使用unsafe关键字编译的代码’"
            // 随后在要使用指针的上下文中使用unsafe关键字。
            //unsafe
            //{
            //    Student s1 = new Student(001, "peter", "男");
            //    Student* ps1 = &s1; // ps1是一个指针，表示s1对象的地址；&表示对变量进行取址操作；p表示pointer。
            //    ps1->Name = "charlie"; // ->为指针操作符。和对象操作符.不同。
            //    (*ps1).Name = "snoppy"; // *ps1表示获取ps1指针所指向的对象，获取到对象后，可以用对象操作符.对其属性进行操作。
            //    Console.WriteLine(s1.Name);
            //}
            #endregion

            #region ~取反操作符
            //int x = 12345;
            //string xBinary = Convert.ToString(x, 2).PadLeft(32,'0');
            //Console.WriteLine(xBinary);
            //int y = ~x; // y=-12346
            //string yBinary = Convert.ToString(y, 2).PadLeft(32, '0');
            //Console.WriteLine(yBinary);
            //int minusX = ~x + 1; //minusX=-12345
            //Console.WriteLine(minusX);
            ////minusX = -x; // 和上面等效
            #endregion

            #region ++ --操作符
            ////int x = 100;
            ////int y = x++; // 先执行赋值，再加。
            //int x = 100;
            //int z = ++x; // 先加，再赋值。
            //Console.WriteLine($"x:{x};z:{z}");
            #endregion

            #region (T)类型转换操作符
            //// 不丢失精度的转换
            //int x = 100;
            ////int y = x; //将long类型的Int64转换成了Int32，可能会发生精度的缺失，因此无法隐式转换。
            //long z = x; //将Int32转换成Int64，不会发生精度缺失，因此可以进行隐式转换。

            //子类向父类转换
            //Student s = new Student(001, "s1", "女");
            //Human h = s; //由Student向Human的隐式类型转换，转换后，h只能访问Human的属性和方法。
            //Animal a = h; //由Human向Animal的隐式类型转换，a只能访问Animal的属性和方法。

            ////强制类型转换（可能造成数据丢失）
            //uint x = 65536; //ushort为Int16，最大值为65535，Int32的65536二进制表示：低16位是0，高16位是000...1；
            //ushort y = (ushort)x; //将Int32强制转换成Int16，只保留低16位0，高16位被舍弃，因此转换后结果是0；
            //Console.WriteLine(y);

            ////自定义显示/隐士类型转换 Stone->WuKong
            //Stone stone = new Stone(500);
            //Monkey wuKong = (Monkey)stone;
            ////or
            ////Monkey macaque = stone;
            //Console.WriteLine($"{wuKong.Age},");
            #endregion

            #region << >>位移操作符
            //int a = 10;
            //int b = a << 1; //相当于*2，在不发生溢出的情况下。
            //int c = a >> 1; //相当于/2。
            //Console.WriteLine($"{b},{c}");
            #endregion

            #region quiz1
            //打印九九乘法表
            //Haskell();
            #endregion

            #region quiz2
            //提示用户输入10个实数，可以是整数也可以是小数，可以是正也可以是负数，每个数之间用空格分隔，然后显示出这10个数里最大的数，平均数，和总和，输入'exit'退出程序。
            //Quiz2();
            #endregion

            #region quiz3
            //随机显示一个加减乘除的式子，例如10*10=，然后让用户输入一个结果，如果结果正确，就显示“结果正确”，如果错误，就显示“错误”，
            //然后再次让用户输入，输入3次错误后，直接显示正确答案；重复以上直到用户输入了'exit'，程序退出。
            //Quiz3V1();
            Quiz3V2();
            #endregion
        }

        static void Quiz2() 
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
                    else if(input.Trim() == "") //"" = "     ".Trim()
                        throw new Exception("输入不能为空");
                    else if (input.Contains("，"))
                        throw new Exception("逗号必须是英文字符");
                    if(input.Equals("exit"))
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

        /// <summary>
        /// 使用Expression tree实现
        /// </summary>
        static void Quiz3V1()
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
                    int op = rnd.Next(1,5);
                    Expression expression = op switch
                    {
                        1 => Expression.AddChecked(leftExp, rightExp),
                        2 => Expression.SubtractChecked(leftExp,rightExp),
                        3 => Expression.MultiplyChecked(leftExp, rightExp),
                        4 => Expression.Divide(leftExp, rightExp),
                        _ => throw new Exception("No operator matched!"),
                    };
                    double groundTrue = Math.Round(Expression.Lambda<Func<double>>(expression).Compile()(),2);
                    //Console.WriteLine($"{expression.ToString()} = ?");
                    string expressionStr = (op,left,right) switch
                    {
                        { op: 1, right: > 0 } =>$"{left} + {right} = ?",
                        { op: 1, right: < 0 } =>$"{left} - {-right} = ?",
                        { op: 2, right: > 0 } =>$"{left} - {right} = ?",
                        { op: 2, right: < 0 } => $"{left} + {-right} = ?",
                        { op: 3, right: > 0 } =>$"{left} * {right} = ?",
                        { op: 3, right: < 0 } =>$"{-left} * {-right} = ?",
                        { op: 4, right: > 0 } =>$"{left} / {right} = ?",
                        { op: 4, right: < 0 } =>$"{-left} / {-right} = ?",
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
                        if(life == 0)
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
        static void Quiz3V2()
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
                        var groundTrue = Math.Round(Double.Parse(groundTrueStr),2);
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
        static void Haskell()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write($"{i}*{j}={i * j}\t");
                }
                Console.WriteLine();
            }
        }

        enum Vehicle
        {
            Car,
            Truck,
            Bus,
            Train
        }

        static float GetToll(Vehicle vehicle) => vehicle switch
        {
            Vehicle.Car => 100f,
            Vehicle.Truck => 200f,
            Vehicle.Bus => 80f,
            _ => throw new ArgumentException("Unknown vehicle", nameof(vehicle))
        };

        static int GetSourceLabel<T>(IEnumerable<T> source) => source switch
        {
            Array array => 1,
            ICollection<T> collection => 2,
            _ => 3,
        };

        static string GetSeason(DateTime date) => date.Month switch
        {
            3 or 4 or 5 => "Spring",
            6 or 7 or 8 => "Summer",
            9 or 10 or 11 => "Autumn",
            12 or 1 or 2 => "Winter",
            _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
        };

        //static string GetSeason(DateTime date) => date.Month switch
        //{
        //    >=3 and <=5 => "Spring",
        //    >=6 and <=8 => "Summer",
        //    >=9 and <=11 => "Autumn",
        //    12 or 1 or 2 => "Winter",
        //    _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
        //};

        class Animal
        {
            public void Eat()
            {
                Console.WriteLine("Eating...");
            }
        }

        //Human类继承Animal类
        class Human : Animal 
        {
            public void Think()
            {
                Console.WriteLine("Thinking...");
            }
        }

        class Student:Human
        {
            public int Id;
            public string Name;
            public string Gender;

            public Student(int id, string name, string gender)
            {
                Id = id;
                Name = name;
                Gender = gender;
            }

            public void Learn()
            {
                Console.WriteLine("Learn...");
            }
        }

        class Stone
        {
            public int Age;

            public Stone(int age)
            {
                Age = age;
            }

            // 显式类型转换，Stone->Monkey
            public static explicit operator Monkey(Stone stone)
            {
                Monkey res = new Monkey(stone.Age / 100);
                return res;
            }

            // 隐式类型转换，Stone->Monkey
            //public static implicit operator Monkey(Stone stone)
            //{
            //    Monkey res = new Monkey(stone.Age / 100);
            //    return res;
            //}
        }

        class Monkey
        {
            public int Age;

            public Monkey(int age)
            {
                Age = age;
            }
        }
    }
}
