//结构体、枚举、数组、枚举器和迭代器

#region struct和class：struct是值类型，不能派生其他结构；class是引用类型，可以派生其他类
//struct存在栈中，而class的引用存在栈中，内部的值类型成员存放在栈中，引用类型成员存放在堆中，其引用存放在栈中。
//struct有隐藏的无参构造，且无法被显示覆盖。程序员可以提供其他的有参构造。
//class在程序员不提供显示构造函数时，具有隐式无参构造，一旦提供了任意的构造函数，就不存在隐式无参构造了。
//ClassSample cs1 = new ClassSample(); //只有当不存在任何显示有参构造时，才存在无参构造，当有任何显示有参构造时，就不存在隐式无参构造了。
//StructSample ss1 = new StructSample(); //调用struct的隐式无参构造.
//StructSample ss2 = new StructSample(1, 2, null);
//cs1.X = cs1.Y = 3;
//Console.WriteLine($"{ss1.X}   {ss1.Y}   {StructSample.Z}   {ss1.Y}   {StructSample.M}");

////struct总是sealed的，因此无法继承。
////struct作为返回值，返回的是其副本，而非本身。
////struct作为值参数，传入的是其副本，而非本身。
////想要返回或传入struct本身，需要使用out或ref关键字
//ReturnAStructWithOut(ss1, out ss1);
//Console.WriteLine($"{ss1.X}  {ss1.Y}");
//UpdateStructWithRef(ref ss1);
//Console.WriteLine($"{ss1.X}  {ss1.Y}");
//UpdateAStruct(ss1); //由于struct是值类型，ss1被传入后，实际被修改的是ss1的副本，而不是ss1本身，因此执行UpdateAStruct()之后ss1保持不变。
//Console.WriteLine($"{ss1.X}  {ss1.Y}");
//总结
//1.通常struct用来表示小型数据结构，通常小于16Byte，如Point{int X, int Y}, DateTime等。
//2.当数据不需要继承时，使用struct。
//3.一旦创建对象后，数据结构不变的情况下，使用struct。
#endregion

#region 枚举：Enum是值类型，存在栈中。
//默认第一个元素值为0，后续依次递增1，类型为int。
//也可给其中任意元素赋值，后续依次递增1。
//TrafficLight t1 = TrafficLight.Yellow;
//TrafficLight t2 = TrafficLight.Red;
//TrafficLight t3 = TrafficLight.Green;
//Console.WriteLine($"{t1} {t2} {((int)t3)}");
//Console.WriteLine($"{((long)TrafficLightLong.Green)} {((long)TrafficLightLong.Yellow)}");
#endregion

#region enum & flag：使用枚举与位标志标记属性
//User Petter = new User("Petter", UserPortrait.Single | UserPortrait.Local);
//User John = new User("John", UserPortrait.VIP | UserPortrait.Local | UserPortrait.Graduate);
//string PetterPortrait = Petter.Portrait.ToString(); //使用[Flag]装饰符，重写ToString()方法。
//Console.WriteLine($"Petter: {Petter.UserValue} \t John:{John.UserValue}");
//Console.WriteLine($"Petter's user portrait: {PetterPortrait} \nQ: Does Petter has attributes Graduate and Local? \nA: {Petter.HasAttributes(UserPortrait.Graduate | UserPortrait.Local)}");
//Console.WriteLine($"John's user portrait: {John.Portrait}");
//Petter.Portrait = UserPortrait.Local;
//Console.WriteLine($"Petter: {Petter.UserValue} \t Petter's user portrait: {Petter.Portrait}");
#endregion

#region 数组(Array)：定长，如果长度总是变化，建议使用List。
//int[] arr1 = new int[5];
//arr1 = [1, 2, 3, 4, 5]; //.net8中可以使用：int[] arr1 = [1, 2, 3, 4, 5];
//int[] arr2 = [1, 2, 3, 4, 5]; //也可以使用花括号。
//int[,] arr3 = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } }; //int[,]表示声明一个二维数组，int[,,]表示声明一个三维数组。
//int[,,] arr4 = new int[2, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } },
//                                { { 7, 8, 9 }, { 10, 11, 12 } }};
////访问元素
//Console.WriteLine(arr2[2]);
//Console.WriteLine(arr3[1, 2]);
//Console.WriteLine(arr4[1, 0, 2]);
//Console.WriteLine($"Rank for arr4: {arr4.Rank}"); //Rank:数组的秩，即维度。
//foreach (int i in arr4)
//{
//    Console.Write($"{i} ");
//}
//Console.WriteLine();
//Console.WriteLine($"0维度元素个数：{arr4.GetLength(0)}");
//Console.WriteLine($"1维度元素个数：{arr4.GetLength(1)}");
//Console.WriteLine($"2维度元素个数：{arr4.GetLength(2)}");
//for (int i = 0; i < arr4.GetLength(0); i++) //GetLength(int dimension)接收维度作为参数，返回该维度的元素个数。
//{
//    for (int j = 0; j < arr4.GetLength(1); j++)
//    {
//        for (int k = 0; k < arr4.GetLength(2); k++)
//        {
//            Console.Write(arr4[i, j, k] + " ");
//        }
//    }
//}
#endregion

#region Array VS List：Struct[]和List<Struct>均无法对原本的Struct元素进行更改，但可以修改集合里的元素，这是由于Struct的ValueType决定的；Class[]和List<Class>均可对集合里的元素和Class对象本身进行更改。
//Struct[] VS List<Struct>
//StructSample a = new();
//a.X = 1;
//List<StructSample> listStruct = [a];
//StructSample[] arrStruct = { a };

//static void UpdateArray(StructSample[] input)
//{
//    input[0].X = 100;
//}

//static void UpdateList(List<StructSample> input)
//{
//    //input[0].X = 200; //编译错误。
//    //想要修改List<struct>的元素，只能使用新的struct替代原来的struct，无法直接修改其值。
//    StructSample temp = new StructSample();
//    temp.X = 200;
//    input[0] = temp;
//}

//UpdateArray(arrStruct);
//Console.WriteLine($"{arrStruct[0].X}   {a.X}"); //只修改了arrStruct[0].X，但是a.X没变。
//UpdateList(listStruct);
//Console.WriteLine($"{listStruct[0].X}   {a.X}"); //只修改了listStruct[0].X，但是a.X没变。

////Class[] VS List<Class>
//ClassSample b = new ClassSample();
//b.X = 1;
//List<ClassSample> listClass = [b];
//ClassSample[] arrClass = { b };

//static void UpdateArrayClass(ClassSample[] input)
//{
//    input[0].X = 100;
//}

//static void UpdateListClass(List<ClassSample> input)
//{
//    input[0].X = 300;
//}

//UpdateArrayClass(arrClass);
//Console.WriteLine($"{arrClass[0].X}   {b.X}"); //同时修改了arrClass[0].X和b.X。
//UpdateListClass(listClass);
//Console.WriteLine($"{listClass[0].X}   {b.X}"); //同时修改了listClass[0].X和b.X。

//总结：
//1.使用一个集合去存储对象，如果这个集合中的元素会被修改，且改变希望能够使原本的对象也相应修改，那么对象最好使用class而不是struct。
//2.如果一个集合的元素个数不确定，或个数会变化，使用List而非Array。
//3.高性能要求、对象数量多、生命周期短，由于struct不使用GC，因此创建和销毁对象的开销远小于class。
//4.如果对象很大，使用class，因为struct在赋值或参数传递时会复制副本，因此如果对象很大，复制的开销会很大。
#endregion

#region 交错数组(Jagged Array)：不同维度的数组元素个数可以不同。
int[][] jagArr1 = new int[3][];
jagArr1[0] = [1, 2, 3];
jagArr1[1] = [1, 2, 3,4];
jagArr1[2] = [1, 2];

int[][] jagArr2 =
[
    [1, 3, 5, 7, 9],
    [0, 2, 4, 6],
    [11, 22]
];
jagArr2[0][1] = 77;

int[][,] jagArr3 =
[
    new int[,] { {1,3}, {5,7} },
    new int[,] { {0,2}, {4,6}, {8,10} },
    new int[,] { {11,22}, {99,88}, {0,9} }
];
Console.WriteLine("{0}", jagArr3[0][1, 0]);
Console.WriteLine(jagArr3.Length);
#endregion

#region 枚举器：Enumerator
#endregion

#region 迭代器：Iterator，通过yield return生成Enumerator; Python中叫生成器(Generator)。
#endregion
static void ReturnAStructWithOut(StructSample input, out StructSample output)
{
    input.X = 100;
    input.Y = 200;
    output = input;
}

//ref：将值参数当做引用参数去处理，在函数内部处理的参数，不是传入参数的拷贝，而是传入参数本身。
static void UpdateStructWithRef(ref StructSample input)
{
    input.X = 300;
    input.Y = 400;
}

static void UpdateAStruct(StructSample ss1)
{
    ss1.X = 400;
    ss1.Y = 500;
}

class A
{
    public int a;
}

class ClassSample
{
    //类的字段和属性均可进行初始化。
    public int X;
    public int Y;
    public A? AObj;

    //如果提供了有参构造函数，就必须也提供一个无参构造函数，否则无法正确调用无参构造。
    //public ClassSample(int x, int y, A? aObj)
    //{
    //    X = x;
    //    Y = y;
    //    AObj = aObj;
    //}
}

struct StructSample
{
    public int X = 10; //无法初始化非静态字段和属性。
    public static int Z = 10; //可以初始化静态字段和属性。
    public int Y { get; set; } = 10;
    public static int M { get; set; } = 20;
    public A? AObj;

    //只需要提供有参构造，不能提供无参构造。
    public StructSample(int x, int y, A? aObj)
    {
        X = x;
        Y = y;
        AObj = aObj;
    }
}

class User
{
    public string Name { get; set; } = null!;
    private UserPortrait _portrait;
    private int _value;

    public UserPortrait Portrait
    {
        get { return _portrait; }
        set { _portrait = value; }
    }

    public int UserValue
    {
        get { return _setValue(); }
        private set { _value = _setValue(); }
    }

    public User(string name, UserPortrait portrait)
    {
        Portrait = portrait;
        Name = name;
        UserValue = _setValue();
    }

    /// <summary>
    /// 两种判断是否具有某些特性的方法。
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool HasAttributes(UserPortrait p)
    {
        return (p & _portrait) == p; //按位与
        //return _portrait.HasFlag(p); //enum.HasFlag()方法
    }

    private int _setValue() => Portrait switch
    {
        UserPortrait.Single | UserPortrait.Graduate | UserPortrait.Local | UserPortrait.VIP => 100, //0x 0*28 1111
        UserPortrait.Graduate | UserPortrait.Local | UserPortrait.VIP => 90,                        //0x 0*28 1110
        UserPortrait.Graduate | UserPortrait.Local => 50,                                           //0x 0*28 0110
        UserPortrait.Local => 30,                                                                   //0x 0*28 0100
        _ => 60
    };
}

/// <summary>
/// 用户画像，使用Flags decoration修饰。
/// Flags标记后就重写了UserPortrait的ToString()方法，使多个enum的组合可以正确显示其文字信息，不写Flags，只能得到几个enum的和。
/// </summary>
[Flags]
enum UserPortrait : uint
{
    Single = 0x01,    //0x 0*28 0001  
    Graduate = 0x02,  //0x 0*28 0010 
    Local = 0x04,     //0x 0*28 0100
    VIP = 0x08        //0x 0*28 1000 
}

enum TrafficLight
{
    Green,
    Red,
    Yellow
}

enum TrafficLightLong : long
{
    Green,
    Red = 5,
    Yellow
}
