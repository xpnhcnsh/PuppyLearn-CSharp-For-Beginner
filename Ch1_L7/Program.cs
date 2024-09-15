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
//GenStructWithRef(ref ss1);
//Console.WriteLine($"{ss1.X}  {ss1.Y}");
//UpdateAStruct(ss1); //由于struct是值类型，ss1被传入后，实际被修改的是ss1的副本，而不是ss1本身，因此执行UpdateAStruct()之后ss1保持不变。
//Console.WriteLine($"{ss1.X}  {ss1.Y}");
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

#region 数组

#endregion


static void ReturnAStructWithOut(StructSample input, out StructSample output)
{
    input.X = 100;
    input.Y = 200;
    output = input;
}

//ref：将值参数当做引用参数去处理，在函数内部处理的参数，不是传入参数的拷贝，而是传入参数本身。
static void GenStructWithRef(ref StructSample input)
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