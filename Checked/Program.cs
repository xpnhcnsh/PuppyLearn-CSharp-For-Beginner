namespace Ch1_L1_Checked
{
    class Program
    {
        static void Main(string[] args)
        {

            //#region 变量
            //string variable1 = "This is variable2";
            //string variable2 = "This is variable2, which is just behind variable1.";
            //Console.WriteLine(variable2);
            //#endregion

            //#region 字符串
            //string variable3 = ", which is just behind variable1.";
            //Console.WriteLine(variable1 + variable3);
            ////字符串差值 string interpolation
            //Console.WriteLine($"Hello world! {variable1}--{variable3}");
            //#endregion

            //#region character 字符
            //char a = ',';
            //Console.WriteLine(a);
            //#endregion

            #region int  integer 整型  
            //int a = 1;
            //int b = 2;
            //int c = a + b;
            //string d = "1";
            //string e = "2";
            //string f = d + e;
            //Console.WriteLine(c);
            //Console.WriteLine(f);
            #endregion

            //#region float(32)/double(64)/decimal(128)
            //int a = 1;
            //float b = 2.2f;
            //float c = a + b;
            //Console.WriteLine(c);
            //#endregion

            #region boolean 与或非
            //bool a = true;
            //bool b = false;
            //bool c = a & b; //与
            //Console.WriteLine(c);
            //bool d = a | b; //或
            //Console.WriteLine(d);
            //bool e = !a; //非
            //Console.WriteLine(e);
            #endregion

            #region switch two values
            //int a = 10;
            //int b = 20;
            //// switch value for a and b. temporal临时的 暂时的
            //int temp = a;
            //a = b;
            //b = temp;
            //Console.WriteLine($"a={a};b={b}");
            #endregion

            #region dynamic动态类型
            //dynamic a = 10;
            //a = "lala";
            //Console.WriteLine(a);
            #endregion

            //#region var编译器可自行推断类型 variable
            //var a = "puppylearn";
            //var b = 10.3d;
            //Console.WriteLine($"a: {a.GetType().Name},b: {b.GetType().Name}");
            //#endregion
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
