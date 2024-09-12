//类：继承、多态、抽象类与开闭原则、接口

using System;
using System.Reflection.Metadata.Ecma335;
#region 继承：类在功能上的拓展
//Type t = typeof(Car);
//Console.WriteLine(t.BaseType.FullName);

////C#中所有对象都是继承于System.Object类
//Console.WriteLine(t.BaseType.BaseType.FullName);
//Console.WriteLine(t.BaseType.BaseType.BaseType == null);
#endregion

#region is关键字：子类实例 is 父类
//Car car = new Car(1, 1);
//Console.WriteLine($"{car is Vechicle}   {car is Object}");
#endregion

#region public, protected, private, internal, protected internal
//1.默认成员的访问级别是private。
//2.派生类的访问级别不能比基类高。即如果基类是protected级别，那么子类只能是protected或private级别，不能是public。
//3.成员的访问级别不能比类高。即如果类是protected级别，那么成员只能是protected或private级别，不能是public。
//4.public: 本程序集和其他程序集，派生类和其他类均可访问。
//5.protected: 本程序集和其他程序集，只有派生类可访问。
//6.private: 本程序集，只有本类内部可以访问。
//7.internal: 本程序集，派生类和其他类均可访问。
//8.protected internal: 本程序集：派生类和其他类均可访问；其他程序集：仅限派生类，其他类不可访问。是protectd和internal的并集，而非交集。

//                     同一程序集内                   不同程序集内
//                     派生  非派生                   派生  非派生
// public               x      x                      x      x
// protected internal   x      x                      x      
// protected            x                             x      
// internal             x      x                           
// private
// 
BaseClass baseClass = new BaseClass();
baseClass.A = 10;
//baseClass.B = 10; //error
//baseClass.C = 10; //error
DerivedClass derivedClass = new DerivedClass();
derivedClass.A = 20;
//derivedClass.B = 20; //error，只能在DerivedClass类内部访问到base.B。
//derivedClass.C = 10; //error
#endregion

#region 多态(polymorphism)：使用父类引用子类实例(子类拥有父类的所有成员(public字段、public属性、public方法))
Vechicle v1 = new Car(1);

//Console.WriteLine(v1.WheelsNum);
//Console.WriteLine(v1.GetGas()); //基类引用后，无法访问子类成员。
//Car c1 = new Car(1);
//Console.WriteLine(c1.WheelsNum);

//Console.WriteLine(c1.GetGas());
//c1.Run(5);
//Console.WriteLine(c1.GetGas());
//c1.Refill(50);
//Console.WriteLine(c1.GetGas());
#endregion

#region virtual & override：基类引用子类实例后，可以访问到子类的成员。
v1.GetSpeed(); //这里虽然是用Vechicle引用Car实例，但调用的GetSpeed()方法是属于Car的。
#endregion

#region 对比virtual和new：虚成员vs子类对父类成员的隐藏
//new和override都可以对virtual成员进行重写/覆盖；override会覆盖所有上层的父类直至基类；new只覆盖一层父类。
BaseClass sdcv1 = new SecondDerivedClassV1();
BaseClass sdcv2 = new SecondDerivedClassV2();
sdcv1.Print(); //使用override重写virtual成员时，当有多层继承，使用基类引用子类实例，调用override方法实际调用了最新的override方法。
sdcv2.Print(); //使用new重写virtual成员时，当有多层继承，使用基类引用子类实例，调用new方法实际调用了最后一层的override方法，也就是DerivedClass这一层。
//总结：
//当有多层继承，且使用基类引用子类实例时：
//1. 当用调用override成员时：override具有传染性，实际会调用最后一层子类的override成员；
//2. 当调用new成员时：实际会调用基类的对应成员（如果基类的该成员，在后续的子类中有该成员对应的override成员，那么调用最后一层子类的override成员）。
#endregion

#region 抽象类：abstract
//抽象类用abstract标识，表示该类必须被继承，且其中的抽象成员必须被子类实现（除非该子类也是抽象类）。
//1.抽象类只能被用于其他类的基类；2.抽象类可以派生自另一个抽象类；3.字段和常量不能声明为abstract。
//AbClass abClass = new AbClass(); //错误，抽象类不能被实例化。
TheDerivedClass theDerivedClassInstance = new TheDerivedClass();
theDerivedClassInstance.IdentifyBase();
theDerivedClassInstance.IdentifyDerived();
#endregion

#region 多态总结
//1.多态基于重写机制：virtual & override。
//2.实例的函数成员的具体行为，取决于的最新override版本。
#endregion

#region 开闭原则(open principle)：除非是因为改bug，否则不要因为业务增加而修改某一个类，即将稳定的，不变的功能封装成类，将不确定的功能封装成抽象成员/抽象类，留给子类去实现。
//基于开闭原则，基类中的virtual方法只需要声明，具体的实现，由子类去完成，当业务拓展，需要写新的子类时，只需要写新的override方法就可以，无需更改基类的方法。
//当一个抽象类中所有的成员都是抽象成员的时候，这个类就被更改为interface接口。因此在interface中，只需要声明抽象成员即可，而实现了某一个接口的类，则需要
//实现其抽象成员（如果只实现一部分抽象成员，则该类依然是一个abstract类）。由于interface的作用是需要被其他类实现，因此interface里所有成员都是public，
//且不能有字段，只能有属性和方法。
//通常讲：继承了某个类；实现了某些接口（一个类可以实现多个接口）。
IVechicle theCar = new TheCar();
theCar.Refill(300);
Console.WriteLine(theCar.GetGas());
theCar.Run(10);
Console.WriteLine(theCar.GetGas());
((ICommodity)theCar).Price = 1000; //商品属性被抽象成一个ICommodity接口，只要实现了该接口的类，都可以具有所有的商品属性。
Console.WriteLine(((ICommodity)theCar).Price);
#endregion


/// <summary>
/// 注意：
/// 1. private类不能作为base class; 2.c#中一个类只能有一个基类； 3.子类的访问级别不能超过父类；
/// 4.子类拥有父类的所有成员(public字段、public属性、public方法)
/// 5.子类可以覆盖父类的成员。
/// 6.子类可以在父类成员的基础上添加成员。
/// 7.一个类被sealed修饰，则无法作为基类。
/// </summary>
public class Vechicle
{
    public int DriverNum = 1;
    public int WheelsNum { get; set; }

    public Vechicle(int driverNum, int wheelsNum)
    {
        DriverNum = driverNum;
        WheelsNum = wheelsNum;
    }

    public Vechicle(int wheelsNum)
    {
        WheelsNum = wheelsNum;
    }

    public void Run(int distance)
    {
        throw new NotImplementedException();
    }

    virtual public void GetSpeed() //虚方法，表示需要被子类重写。
    {
        Console.WriteLine("get vechicle speed...");
    }
}

public class Car : Vechicle
{
    public new int WheelsNum; //隐藏基类成员字段，注意使用new关键字。
    private int _gasTank = 50; //在基类成员基础上添加新的成员字段。
    private readonly int _gcr = 2; //gas comsumption ratio

    /// <summary>
    /// Car构造函数通过base()方法，直接调用父类Vechicle的构造函数，可以在构造函数中重写。
    /// </summary>
    /// <param name="wheelsNum"></param>
    public Car(int wheelsNum) : base(wheelsNum)
    {
        //WheelsNum = wheelsNum + 2; //如果不需要重写，留空。
    }

    public void Refill(int value) //在基类成员基础上添加新的成员方法。
    {
        _gasTank = _gasTank + value;
    }

    public new void Run(int distance) //隐藏基类成员方法，注意使用new关键字。
    {
        _gasTank = _gasTank - _gcr * distance;
    }

    public int GetGas() //在基类成员基础上添加新的成员方法。
    {
        return _gasTank;
    }

    public int GetDriverNum()
    {
        return base.DriverNum; //通过base访问父类的成员，只能向上访问一层。
    }

    public override void GetSpeed() //override方法，表示重写了基类对应的虚方法。
    {
        Console.WriteLine("get car speed...");
    }
}

public class BaseClass
{
    public int A = 1;
    protected int B = 2;
    private int C = 3;
    virtual public void Print()
    {
        Console.WriteLine("This is the base class...");
    }
}

public class DerivedClass : BaseClass
{
    public override void Print()
    {
        //base.B = B; //可以访问到protected级别的字段。
        //base.C = 20; //error，private级别只能在base内部访问。
        Console.WriteLine("This is derived class...");
    }
}

public class SecondDerivedClassV1 : DerivedClass
{
    /// <summary>
    /// 使用override重写virtual方法。
    /// </summary>
    public override void Print()
    {
        Console.WriteLine("This is SecondDerivedClassV1 class...");
    }
}

public class SecondDerivedClassV2 : DerivedClass
{
    /// <summary>
    /// 使用new重写virtual方法。
    /// </summary>
    public new void Print()
    {
        Console.WriteLine("This is SecondDerivedClassV2 class...");
    }
}

/// <summary>
/// Abstract class, 有一个concrete method 和一个 abstract method。
/// </summary>
abstract public class AbClass
{
    /// <summary>
    /// Concrete Method；如果当前实例是AbClass类型，则打印，如果不是，则打印当前实例父类的类型。
    /// </summary>
    public void IdentifyBase()
    {
        Type t = this.GetType();
        if (t == typeof(AbClass))
        {
            Console.WriteLine(t);
        }
        else
        {
            Console.WriteLine(this.GetType().BaseType?.FullName);
        }

    }

    /// <summary>
    /// Abstract Method；抽象方法，只声明，不实现，由子类去实现。如果有函数体将报错。C++中被称为纯虚方法。
    /// </summary>
    abstract public void IdentifyDerived();
}

class TheDerivedClass : AbClass
{
    /// <summary>
    /// 必须重写父类中的abstract方法。获取当前类的名称。
    /// </summary>
    public override void IdentifyDerived()
    {
        Console.WriteLine(this.GetType().FullName);
    }
}

interface IVechicle
{
    void Run(int distance);
    int GetSpeed();
    int Refill(int value);
    int GetDriverNum();
    int GetGas();
    void SpeedUp();
}

interface ICommodity
{
    public int Price {  get; set; }
}


class TheCar : IVechicle, ICommodity
{
    private readonly int _driverNum = 1;
    private int _gasTank = 0;
    private int _speed = 0;
    private readonly int _gcr = 2; //gas comsumption ratio
    private const int _GasTankMax = 100;
    private int _price = 100;

    public int GasTank
    {
        get
        {
            return _gasTank;
        }
        private set
        {
            _gasTank = value >= _GasTankMax ? _GasTankMax : value;
        }
    }
    public int Speed
    {
        get
        {
            return _speed;
        }
        private set
        {
            _speed = value > 240 ? 240 : value;
        }
    }

    int ICommodity.Price { get => _price; set => _price = value; }

    int IVechicle.GetDriverNum()
    {
        return _driverNum;
    }

    int IVechicle.GetGas()
    {
        return _gasTank;
    }

    int IVechicle.GetSpeed()
    {
        return _speed;
    }

    int IVechicle.Refill(int value)
    {
        GasTank += value;
        return GasTank;
    }

    void IVechicle.Run(int distance)
    {
        GasTank -= distance * _gcr;
        GasTank = GasTank <=0? 0: GasTank;
    }

    public void SpeedUp()
    {
        Speed += 1;
    }
}

