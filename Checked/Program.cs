namespace Ch1_L1_Checked
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            #region 变量
            string variable1 = "This is variable1.";
            string variable2 = "This is variable2, which is just behind variable1";
            Console.WriteLine(variable1);
            #endregion

            #region 字符串
            Console.WriteLine(variable1 + variable2);
            //字符串差值 string interpolation
            Console.WriteLine($"{variable1}{variable2}");
            #endregion

            //#region char
            //char a = 'a';
            //Console.WriteLine(a);
            //#endregion

            //#region int
            //int a = 1;
            //int b = 2;
            //int c = a + b;
            //Console.WriteLine(c);
            //#endregion

            //#region float(32)/double(64)/decimal(128)
            //float a = 0.1f;
            //float b = 2.2f;
            //float c = a + b;
            //Console.WriteLine(c);
            //#endregion

            //#region boolean
            //bool a = true;
            //bool b = false;
            //bool c = a&b;
            //bool d = a|b;
            //bool e = !a;
            //#endregion

            //#region switch two values
            //int a = 10;
            //int b = 20;
            //// switch value for a and b.
            //int temp = a;
            //a = b;
            //b = temp;
            //Console.WriteLine($"a={a};b={b}");
            //#endregion

            //#region dynamic动态类型
            //dynamic a = 10;
            //a = "lala";
            //Console.WriteLine(a);
            //#endregion

            #region var编译器可自行推断类型
            var a = "puppylearn";
            var b = 10.3d;
            Console.WriteLine($"a: {a.GetType().Name},b: {b.GetType().Name}");
            #endregion
        }
    }

    public class Person
    {
        public Person(string name, int age, string gender, DateOnly birthday)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Birthday = birthday;
        }

        public override string ToString()
        {
            return $"Name:{Name}, Age:{Age}, Gender:{Gender}, Birthday:{Birthday}";
        }

        public static int LegCount = 4;

        public static int GetLegCount()
        {
            return 4;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateOnly Birthday { get; set; }
    }
    public struct CoordsStruct
    {
        public CoordsStruct(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public readonly double Sum()
        {
            return X + Y;
        }
    }
    public class CoordsClass
    {
        public CoordsClass(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public double Sum()=>X+Y;
    }
}
