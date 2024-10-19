//结构体、枚举、数组、枚举器和迭代器
using System.Collections;
#region struct和class：struct是值类型，不能派生其他结构；class是引用类型，可以派生其他类
//struct存在栈中，class的引用存在栈中。对于struct和class：内部的值类型成员存放在栈中，引用类型成员存放在堆中，其引用存放在栈中。
//struct有隐藏的无参构造，且无法被显示覆盖。程序员可以提供其他的有参构造。
//class在程序员不提供显示构造函数时，具有隐式无参构造，一旦提供了任意的构造函数，就不存在隐式无参构造了。
//ClassSample cs1 = new ClassSample(); //只有当不存在任何显示有参构造时，才存在无参构造，当有任何显示有参构造时，就不存在隐式无参构造了。
StructSample ss1 = new StructSample(); //调用struct的隐式无参构造.
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
//4.当对象会被经常传入函数进行计算时，使用class。
#endregion

#region 枚举：Enum是值类型，存在栈中
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
//int[] arr1 = new int[5]; //数组声明
//arr1 = [1, 2, 3, 4, 5];  //数组赋值 
//int[] arr2 = [1, 2, 3, 4, 5]; //.net8:声明+赋值
////int[] arr = new int[] { 1, 2, 3, 4, 5 }; //也可以使用花括号。
//int[,] arr3 = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } }; //int[,]表示声明一个二维数组，int[,,]表示声明一个三维数组。
//int[,,] arr4 = new int[2, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } },
//                               { { 7, 8, 9 }, { 10, 11, 12 } }};
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

#region Array VS List：Struct[]和List<Struct>均无法对原本的Struct元素进行更改，但可以修改集合里的元素，这是由于Struct的ValueType决定的；Class[]和List<Class>均可对集合里的元素和Class对象本身进行更改
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
//    //input[0] = new StructSample { X = 200 };
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
//1.使用一个集合去存储对象，如果这个集合中的元素会被修改，且改变希望能够使原本的对象也相应修改，那么对象使用class而不是struct。
//2.如果一个集合的元素个数不确定，或个数会变化，使用List而非Array。
//3.高性能要求、对象数量多、生命周期短，由于struct不使用GC，因此创建和销毁对象的开销远小于class。
//4.如果对象很大，使用class，因为struct在赋值或参数传递时会复制副本，因此如果对象很大，复制的开销会很大。
#endregion

#region 交错数组(Jagged Array)：不同维度的数组元素个数可以不同
//int[][] jagArr1 = new int[3][]; //
//jagArr1[0] = [1, 2, 3];
//jagArr1[1] = [1, 2, 3, 4];
//jagArr1[2] = [1, 2];

////int[][] jagArr2 =
////[
////    [1, 3, 5, 7, 9],
////    [0, 2, 4, 6],
////    [11, 22]
////];
////jagArr2[0][1] = 77;

//int[][,] jagArr3 =
//[
//    new int[,] { {1,3}, {5,7} },
//    new int[,] { {0,2}, {4,6}, {8,10} },
//    new int[,] { {11,22}, {99,88}, {0,9} }
//];
//Console.WriteLine("{0},{1}", jagArr3[0][1, 0], jagArr3[0][0, 0]);
//Console.WriteLine($"{jagArr3[0][1, 0]}");
//Console.WriteLine(jagArr3.Length);
#endregion

#region 枚举器：Enumerator
//实现了IEnumerable接口的类，可以使用for loop或foreach语句去循环遍历，原因是其中的GetEnumerator()方法返回一个枚举器，枚举器可以依次返回集合中的一个对象。
//实现了GetEnumerator()方法的类，称为可枚举类型IEnumerable。
//Enumerator：枚举器
//IEnumerable：可枚举类型
//两者的关系为：Enumerator = IEnumerable.GetEnumerator()。
//Enumerator具有三个成员：Current属性、MoveNext方法、Reset方法。
//Current：只读属性，返回序列中当前位置的元素。
//MoveNext：把枚举器的位置前进到序列中的下一项；返回bool值，指示新的位置是否越界。枚举器的初始位置是第一项之前，因此在第一次使用Current前，需要先调用一次MoveNext。
//Reset：把序列的位置重置为初始位置。

//使用GetEnumerator()方法获取Enumerator，并使用迭代器实现ForEach：

//int[] arr = [10, 2, 3, 12, 2, 3];

//foreach (int i in arr)
//    Console.Write(i + " ");

//Console.WriteLine();

//IEnumerator enumerator = arr.GetEnumerator();
//while (enumerator.MoveNext())
//{
//    int item = (int)enumerator.Current;
//    Console.Write(item + " ");
//}
#endregion

#region 在类中实现IEnumerator和IEnumerable
//1.创建一个枚举器，即写一个类，实现IEnumerator接口。
//2.创建一个类，实现IEnumerable接口，在其中的GetEnumerator()方法中，返回自己写的枚举器。这个类就可以和List或Array一样使用ForEach去遍历了。
//RainBowV1 rainBowV1 = new RainBowV1();
//Console.WriteLine();
//foreach (string color in rainBowV1)
//    Console.Write(color + " ");
#endregion

#region 迭代器：Iterator，通过yield return生成Enumerator; Python中叫生成器(Generator)
//无需实现IEnumerator中的属性和方法即可生成Enumerator。
//RainBowV2 rainBowV2 = new RainBowV2();
//Console.WriteLine();
//foreach (string color in rainBowV2)
//    Console.Write(color + " ");
//foreach (string color in rainBowV2.Colors())
//    Console.Write(color + " ");
#endregion

#region 迭代器：yield return本质是延迟执行(延迟计算)，只有需要获取计算结果时，才执行yield return语句
//假设有一个集合，里面有无限的元素，如果需要遍历这个集合，传统方法是将这个集合全部放在内存中，然后使用指针去内存遍历；
//但内存是有限的，无限个元素实际上无法被放到内存里，这时就需要延迟计算，即只有当遍历到某个元素时，才去计算它并存放在内存中，然后再去遍历它，
//集合中没有被遍历到的元素，都不需要提前计算好存入内存。这样即使有一个很大的集合，也不需要在实际需要某个元素之前，就把集合整体都存放在内存中，从而节省内存。
//使用return遍历斐波那契数列：
//运行后打开Task Manager，会发现内存使用量很快到100%且电脑可能会因此卡住。这是因为List过大将内存占满了。注意观察内存使用情况，及时关闭程序！！！！
//foreach (long i in FibonacciV1(99999999999999))
//    Console.WriteLine(i + " ");

//使用yield return编列斐波那契数列：
//发现内存占用并没有明显升高，程序运行正常
//foreach (long i in FibonacciV2(99999999999999))
//{
//    //可以在一个遍历一个无限序列时加入一些判断逻辑，当符合逻辑时，跳出遍历。
//    //if (i > 1000)
//    //{
//    //    break;
//    //}
//    Console.WriteLine(i + " ");
//}
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

//使用List去保存生成的数据，Bad practice。
static List<long> FibonacciV1(long n)
{
    List< long> temp = new List<long>();
    long current = 0;
    long next = 1;
    while (n > 0)
    {
        temp.Add(current);
        long oldCurrent = current;
        current = next;
        next = next + oldCurrent;
        n--;
    }
    return temp;
}

//使用yield return生成前n项斐波那契，Good practice。
static IEnumerable<long> FibonacciV2(long n)
{
    long current = 0;
    long next = 1;
    while (n > 0)
    {
        yield return current;
        long oldCurrent = current;
        current = next;
        next = next + oldCurrent;
        n--;
    }
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
        //return (p & _portrait) == p; //按位与
        return _portrait.HasFlag(p); //enum.HasFlag()方法
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
    Green = 0,
    Red = 5,
    Yellow
}

/// <summary>
/// 实现了IEnumerator的类即为枚举器，具体需要实现Current属性、MoveNext方法和Reset方法。
/// </summary>
class ColorEnumerator : IEnumerator
{
    private string[] _colors = null!;
    int position = -1; //当前位置初始化为-1。

    public ColorEnumerator(string[] colors)
    {
        _colors = colors;
    }

    public object Current
    {
        get
        {
            if (position == -1)
                throw new IndexOutOfRangeException();
            if (position > _colors.Length)
                throw new IndexOutOfRangeException();
            return _colors[position];
        }
    }

    public bool MoveNext()
    {
        if (position < _colors.Length - 1)
        {
            position++;
            return true;
        }
        return false;
    }

    public void Reset()
    {
        position = -1;
    }
}

/// <summary>
/// 实现了IEnumerable的类可以通过GetEnumerator()方法返回一个枚举器，从而使用ForEach去遍历。
/// </summary>
class RainBowV1 : IEnumerable
{
    string[] _colors = ["violet", "blue", "cyan", "green", "yellow", "orange", "red"];
    public IEnumerator GetEnumerator()
    {
        return new ColorEnumerator(_colors);
    }
}

/// <summary>
/// 在使用GetEnumerator()中使用yield return方法返回IEnumerator，无需先写一个实现了IEnumerator的类了。
/// </summary>
class RainBowV2 : IEnumerable
{
    string[] _colors = ["violet", "blue", "cyan", "green", "yellow", "orange", "red"];
    public IEnumerator GetEnumerator()
    {
        foreach (var item in _colors)
            yield return item;
    }

    public string[] Colors()
    {
        return _colors;
    }
}