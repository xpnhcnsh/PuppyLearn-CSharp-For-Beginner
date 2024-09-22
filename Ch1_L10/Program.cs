//泛型：泛型类、泛型方法、泛型结构、泛型委托、泛型接口、协变和逆变
using System;
using System.Collections;

#region 泛型：将类型参数化(type-parameterized)，使用类型占位符写代码，在创建实例时才指明真实的类型。
//MyStackV1 myStackV1 = new MyStackV1();
//myStackV1.Push(1);
//myStackV1.Push(2);
//myStackV1.Push(3);
//Console.WriteLine(myStackV1.Pop());
//myStackV1.PrintStatck();

//MyStackV2<double> myStackV2 = new(); //使用<double>指定真实的类型。
//myStackV2.Push(1.1);
//myStackV2.Push(2.2);
//myStackV2.Push(3.3);
//Console.WriteLine(myStackV2.Pop(out double res));
//myStackV2.PrintStatck();
#endregion

#region 泛型约束：还有很大提升空间
//详见 https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters?redirectedfrom=MSDN
//Roster类使用了泛型约束，使T只能为实现了IPerson接口的类。可以有多种不同的约束存在。
//Roster<Student> myRoster = new Roster<Student>();
//myRoster.Add(new Student(3, "Peter"));
//myRoster.Add(new Student(1, "Sam"));
//myRoster.Add(new Student(2, "Jay"));
//myRoster.Print();
//myRoster.Sort();
//myRoster.Print();
#endregion

#region 泛型方法
//int[] intArray = [5, 10, 99, 1, 2];
//string[] stringArray = ["C++", "C#", "Java", "TypeScript"];
//IPerson[] studentArray = [new Student(3, "Peter"), new Student(1, "Sam"), new Student(2, "Jay")];
//Utility.SortNPrint(intArray);
//Utility.SortNPrint<string>(stringArray); //泛型可省略。
//Utility.SortNPrint<IPerson>(studentArray);
#endregion

#region 扩展方法中的泛型
//Roster<Student> students = new Roster<Student>();
//Roster<Teacher> teachers = new Roster<Teacher>();
//students.Add(new Student(3, "Peter"));
//students.Add(new Student(1, "Sam"));
//students.RosterType = "Student";
//teachers.Add(new Teacher(2, "Jay"));
//teachers.RosterType = "Teacher";
////注意：MyClass的声明中，第二个泛型U是IPerson类型，因此所有实现了IPerson接口的类，都可以和Roster<Student>组合成一个Class类型
//MyClass<Student, Teacher> class1 = students.ConstructAClass(teachers);
//class1.OtherRoster!.Print();
//class1.StudentRoster.Print();
#endregion

#region 泛型结构
//var intData = new PiceOfData<int>(10);
//var stringData = new PiceOfData<string>("hello");
//Console.WriteLine($"intData: {intData.Data}");
//Console.WriteLine($"stringData: {stringData.Data}");
#endregion

#region 泛型委托
//泛型类声明
//var mySum = new Func<int, int, int>(Calculator.Sum);
//Console.WriteLine(mySum(10, 15)); 
//var myProduct = Calculator.Product;
//Console.WriteLine(myProduct(10, 15));
#endregion

#region 泛型接口
//Simple trivial = new Simple();
//Console.WriteLine(trivial.ReturnIt(10));
//Console.WriteLine(trivial.ReturnIt("Hello"));
#endregion

#region 协变与逆变(convariance & contravariance)：只针对委托和接口
#region 协变:委托
static Dog MakeDog()
{
    return new Dog();
}
Factory<Dog> dogMaker = MakeDog; //创建一个委托dogMaker。
Factory<Animal> animalMaker = dogMaker; //创建一个新的委托animalMaker，其指向dogMaker。
//注意，dogMaker的参数是Dog类型，是Animal的子类；animalMaker的参数是Animal类型，是Dog的父类。
//如果委托中没有out关键字，那么84行会报错，提示"Can't implicitly convert from Factory<Dog> to Factory<Animal>"。
//dogMaker委托返回值是Dog类型，按理说Dog类型可以使用一个父类(Animal类)去接收，但实际上这里的关系是Factory<Dog>和Factory<Animal>，而非Dog和Animal。
//在委托中返回值是泛型T，如果要用一个派生程度小的泛型委托去接收一个派生程度大的泛型委托，需要在委托中用out关键字标记泛型参数。
#endregion

#region 逆变：委托
static void ActionOnAnimal(Animal a)
{
    Console.WriteLine(a.Legs);
}
Action1<Animal> act1 = ActionOnAnimal;
Action1<Dog> dog1 = act1;
dog1(new Dog());
//如果委托中没有in关键字，那么97行会报错，提示"Can't implicitly convert from Action1<Animal> to Action1<Dog>"。
//在委托中参数是泛型T，如果要用一个派生程度大的泛型委托去接收一个派生程度小的泛型委托，需要在委托中用in关键字标记泛型参数。
#endregion

#region 协变：接口
//这里只看第一个泛型参数。
SimpleReturn<Dog, Animal> dogReturner = new SimpleReturn<Dog, Animal>();
dogReturner.items[0] = new Dog();
IMyInterface<Animal, Animal> animalReturner = dogReturner; //IMyInterface<T1,T2>的T1参数需要标记为out，否则报错。
#endregion

#region 逆变：接口
//这里只看第二个泛型参数。
SimpleReturn<Dog, Animal> animalReturner2 = new SimpleReturn<Dog, Animal>();
IMyInterface<Dog, Dog> DogReturner2 = animalReturner2; //IMyInterface<T1,T2>的T2参数需要标记为in，否则报错。
DogReturner2.SetFirt(new Dog());
#endregion

//总结：
//1.协变与逆变只针对接口和委托。
//2.只发生在接口/委托A被赋值给接口/委托B，且A和B的泛型参数属于相互派生关系。 B = A。
//3.如果泛型参数是返回值，且B的派生程度小于A，这时使用out标记泛型参数，表示协变，即隐式将子类转换成父类。
//4.如果泛型参数是输入参数，且B的派生程度大于A，这时使用in标记泛型参数，表示逆变，即隐式将父类转换成子类。
#endregion

/// <summary>
/// 只能用于存int的stack。如果需要针对其他类型，那么每种类型都需要重新实现。
/// </summary>
/// 
class MyStackV1
{
    private int _statckPointer = 0;
    private int[] _statckArray;
    private const int _maxStack = 10;
    public bool IsStatckFull { get { return _statckPointer >= _maxStack; } }
    public bool IsStatckEmpty { get { return _statckPointer <= 0; } }

    public void Push(int x)
    {
        if (!IsStatckFull)
            _statckArray[_statckPointer++] = x;
    }

    public int Pop()
    {
        return IsStatckEmpty ? _statckArray[0] : _statckArray[--_statckPointer]; //当_statckArray为空，应该返回null而不是_statckArray[0]
    }

    public MyStackV1()
    {
        _statckArray = new int[_maxStack];
    }

    public void PrintStatck()
    {
        for (int i = _statckPointer - 1; i >= 0; i--)
        {
            Console.WriteLine($"Value: {_statckArray[i]}");
        }
    }
}

/// <summary>
/// 泛型版本的MyStack
/// </summary>
/// <typeparam name="T"></typeparam>
class MyStackV2<T>
{
    private int _statckPointer = 0;
    private T[] _statckArray;
    private const int _maxStack = 10;
    public bool IsStatckFull { get { return _statckPointer >= _maxStack; } }
    public bool IsStatckEmpty { get { return _statckPointer <= 0; } }

    public void Push(T x)
    {
        if (!IsStatckFull)
            _statckArray[_statckPointer++] = x;
    }

    //default:Value Type->0,Ref Type->Null
    public bool Pop(out T? res)
    {
        if (IsStatckEmpty)
        {
            res = default;
            return false;
        }
        else
        {
            res = _statckArray[--_statckPointer];
            return true;
        }
    }

    public MyStackV2()
    {
        _statckArray = new T[_maxStack];
    }

    public void PrintStatck()
    {
        for (int i = _statckPointer - 1; i >= 0; i--)
        {
            Console.WriteLine($"Value: {_statckArray[i]}");
        }
    }
}

/// <summary>
/// 使用Where语句对泛型进行约。
/// 这里约束T为IPerson，也可以约束为其他接口或类型，只要其含有int Id和string Name即可。
/// </summary>
/// <typeparam name="T"></typeparam>
class Roster<T> where T : IPerson
{
    private const int _maxCount = 10;
    private ArrayList _roster = new ArrayList(); //使用ArrayList存储数据，其使用object作为元素的类型，因此在排序时，元素本身需要具有IComparable接口。
    public ArrayList Value { get => _roster; set { _roster = value; } }
    public string RosterType { get; set; } = null!;

    public void Add(IPerson input)
    {
        if (_roster.Count < _maxCount)
        {
            _roster.Add(input);
        }
    }

    public void Print()
    {
        Console.WriteLine($"Roster type: {RosterType}");
        foreach (IPerson i in _roster)
        {
            Console.WriteLine($"Id: {i.Id}, Name:{i.Name}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// _roster.Sort()如果需要正确执行，Student类需要实现IComparable或IComparable<T>接口，否则编辑器无法得知如何对Student比较大小从而无法排序。
    /// </summary>
    public void Sort()
    {
        _roster.Sort();
    }
}

/// <summary>
/// IPerson<T>是一个泛型接口，其Id属性的类型是T。
/// </summary>
/// <typeparam name="T"></typeparam>
interface IPerson : IComparable, IComparable<IPerson>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

abstract class Person : IPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;


    /// <summary>
    /// 实现IComparable<IPerson>接口。
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public int CompareTo(IPerson? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException("other can't be null!");
        }

        return Id - other.Id switch
        {
            < 0 => -1,
            > 0 => 1,
            _ => 0
        };
    }

    /// <summary>
    /// 实现IComparable接口。
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public int CompareTo(object? obj)
    {
        if (obj != null && !(obj is IPerson))
            throw new ArgumentException("Object must be of type IStudent!");
        return CompareTo(obj as IPerson);
    }

    public Person(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name:{Name}";
    }
}

class Student : Person
{
    public Student(int id, string name) : base(id, name)
    {
        Id = id;
        Name = name;
    }
}

class Teacher : Person
{
    public Teacher(int id, string name) : base(id, name)
    {
        Id = id;
        Name = name;
    }
}

class Utility
{
    static public void SortNPrint<T>(T[] arr)
    {
        Array.Sort(arr);
        foreach (T t in arr)
            Console.Write($"{t.ToString()}, ");
        Console.WriteLine();
    }
}

/// <summary>
/// 班级由学生列表，和其他列表组成，OtherRoster的泛型为IPerson，因此所有实现了IPerson的类都可以成为OtherRoster。例如可以是Teacher，或者Stuff。
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
class MyClass<T, U> where T : Student where U : IPerson
{
    public Roster<T> StudentRoster { get; set; } = null!;
    public Roster<U>? OtherRoster { get; set; }
}

static class RosterExtension
{
    /// <summary>
    /// Merge roster<Student> and roster<IPerson> to construct a MyClass. roster<IPerson> represents any collection of person other than Student.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="t"></param>
    /// <param name="u"></param>
    /// <returns></returns>
    public static MyClass<T, U> ConstructAClass<T, U>(this Roster<T> t, Roster<U> u) where T : Student where U : IPerson
    {
        return new MyClass<T, U> { StudentRoster = t, OtherRoster = u };
    }
}

struct PiceOfData<T>
{
    private T _data;
    public T Data { get { return _data; } set { _data = value; } }

    public PiceOfData(T data)
    {
        Data = data;
    }
}

/// <summary>
/// T1和T2是参数类型，TR是返回值类型。
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
/// <typeparam name="TR"></typeparam>
/// <param name="t1"></param>
/// <param name="t2"></param>
/// <returns></returns>
public delegate TR Func<T1, T2, TR>(T1 t1, T2 t2);

class Calculator
{
    public static int Sum(int t1, int t2)
    {
        return t1 + t2;
    }

    public static string Product(float t1, double t2)
    {
        return (t1 * t2).ToString();
    }
}

interface IMyIfc<T>
{
    T ReturnIt(T Value);
}

/// <summary>
/// 实现了同一个接口的不同泛型版本。
/// </summary>
class Simple : IMyIfc<int>, IMyIfc<string>
{
    public int ReturnIt(int Value)
    {
        return Value;
    }

    public string ReturnIt(string Value)
    {
        return Value;
    }
}

class Animal { public int Legs = 4; }
class Dog : Animal { }

delegate T Factory<out T>(); //无参数，返回值为T型。out关键字指定了T的协变。

delegate void Action1<in T>(T a); //参数为类型T，无返回值。in关键字指定了T的逆变。

interface IMyInterface<out T1, in T2> //out标记了T1的协变，in标记了T2的逆变。
{
    T1 GetFirst(); //返回值为T1类型。
    void SetFirt(T2 value); //参数类型为T2。
}

class SimpleReturn<T1, T2> : IMyInterface<T1, T2>
{
    public T1[] items = new T1[2];
    public T2[] items2 = new T2[2];
    public T1 GetFirst()
    {
        return items[0];
    }

    public void SetFirt(T2 value)
    {
        items2[0] = value;
    }
}