#region 委托：delegate
//c#中的委托相当于c/c++中的函数指针。
//变量（数据）是以某个地址为起点的一段内存中所存储的值。
//函数（算法）是以某个地址为起点的一段内存中所存储的一组机器语言指令。
//直接调用：通过函数名来调用函数，直接获得函数所在的地址，并调用函数。
//间接调用：通过函数指针来调用函数，先通过函数指针，获得函数的地址，再去该地址执行这组机器语言指令。
#endregion

#region Action: No return value
Calculator Cal = new Calculator();
//Action Act = new Action(Cal.Version); //传入的是需要调用的函数的地址
//Cal.Version(); //直接调用
//Act.Invoke(); //间接调用
//Act(); //简写
//Action<string> Act2 = Cal.ShowName;
//Act2("卡西欧");
//Act2.Invoke("卡西欧");
#endregion

#region Func: Has return value
//Func<int, int, int> func1 = new Func<int, int, int>(Cal.Add);
//Func<int, int, int> func2 = Cal.Sub;
//int x = 100;
//int y = 200;
//int z = 0;
//z = func1(x, y);
//z = func1.Invoke(x, y);
//z = func2.Invoke(x, y);
//z = func2(x, y);
#endregion

#region 自定义delegate
//Calculator cal = new Calculator();
////我们自定义的委托，返回值是一个int，参数是两个int，而Add和Sub方法都满足这个情况，因此这两个方法都可以用我们的委托进行间接调用。
//CalcDelegate cal1 = cal.Add;
//CalcDelegate cal2 = new CalcDelegate(cal.Sub);
//int x = 100;
//int y = 200;
//int z = 0;

//z = cal1.Invoke(x, y);
//z = cal1(x, y);
//z = cal2.Invoke(x, y);
#endregion

#region 委托的使用场景
////1.模板方法：通常委托有返回值；位于代码中部；相当于方法的占位符，在某个方法中，需要处理数据的逻辑不确定，需要在调用该方法时才能确定究竟要如何处理数据。
////2.回调(callback)方法：委托无返回值；位于代码尾部。
//ProductFactory productFactory = new ProductFactory();
//WrapFactory wrapFactory = new WrapFactory();
//Logger logger = new Logger();

//////作者B在这里封装委托
//Func<Product> PizaDelegate = new Func<Product>(productFactory.MakePizza);
//Func<Product> ToyCarDelegate = new Func<Product>(productFactory.MakeToyCar);
//Action<Product> log = new Action<Product>(logger.Log);

//////作者B调用作者A写的wrapFactory.WrapProduct方法，其中的参数都是作者B自己实现的。
//Box box1 = wrapFactory.WrapProduct(PizaDelegate, log);
//Box box2 = wrapFactory.WrapProduct(ToyCarDelegate, log);

//Console.WriteLine($"{box1.Product.Name},\n{box2.Product.Name}");

////以上作者A和B的写作过程，实现了解耦。通常作者A的工作，是实现一个框架，而作者B的工作，是使用该框架实现具体的功能。


//另一个例子：
//框架作者写一个GotoStation()方法，第一步：买票；第二步；去车站；第三步：坐车。
//作为框架的作者，定义了第一步和第三步的具体方法，但第二步由使用框架的人自己去实现。
//void GotoStation(Action Step2)
//{
//    Console.WriteLine();
//    Step1(); //在12306上买票
//    Step2.Invoke();
//    Step3(); //坐车的方法也写死了
//}

//void Step1()
//{
//    Console.WriteLine("Buy tickets on 12306...");
//}

//void Step3()
//{
//    Console.WriteLine("On board...");
//}

////用户A自己写了TakeBus方法，并封装成了Action，作为参数传入GotoStation方法中。
//void TakeBus()
//{
//    Console.WriteLine("go to station by bus...");
//}
//Action step2 = new Action(TakeBus);
//GotoStation(step2);

////用户B自己写了TakeSubway方法，封装成了Action，作为参数传入GotoStation方法中。
//void TakeSubway()
//{
//    Console.WriteLine("go to station by subway...");
//}
//Action takeSubway = new Action(TakeSubway);
//GotoStation(takeSubway);

////用户C用Lambda表达式的方式创建Action并传入GotoStation
//Action byCar = new Action(() => Console.WriteLine("go to station by car..."));
//GotoStation(byCar);

//////使用过程中，用户A、B和C不需要关心step1和step3的实现，之需要自己实现step2的方法即可。
//////而框架使用者不需要关心step2，只需要提前实现好step1和step3即可。
#endregion

#region 多播委托：Multicast Delegate，一个委托中传入多个方法。
//Student stu1 = new Student() { Id = 1, PenColor = ConsoleColor.Green };
//Student stu2 = new Student() { Id = 2, PenColor = ConsoleColor.Blue };
//Student stu3 = new Student() { Id = 3, PenColor = ConsoleColor.Red };

//Action act1 = new Action(stu1.DoHomeWork);
//Action act2 = new Action(stu2.DoHomeWork);
//Action act3 = new Action(stu3.DoHomeWork);

//act1.Invoke();
//act2.Invoke();
//act3.Invoke();

//act1是一个多播委托；执行顺序和封装顺序一致。
//act1 += stu2.DoHomeWork;
//act1 += stu3.DoHomeWork;
//act1 += act3 += act2;
//act1.Invoke();
//act1 -= act3;
//act1();

//如果一个多播委托具有参数，那么里面的每个方法都传入相同的参数。
//如果一个多播委托具有返回值，那么只会返回最后一个方法的返回值，前面的返回值会被忽略。
//如果多播委托的参数是引用参数，那么调用列表中的方法传入的参数是上一个方法修改后的引用参数，而不是原始的引用参数。

//DelWithRefPara cascadingAddRef = AddBy3Ref;
//cascadingAddRef += AddBy2Ref;
//int a = 1;
//cascadingAddRef(ref a);
//Console.WriteLine(a);

//Action<int> cascadingAdd = AddBy3;
//cascadingAdd += AddBy2;
//int b = 1;
//cascadingAdd(b);
//Console.WriteLine(b);

void AddBy3Ref(ref int x)
{
    x += 3;
}

void AddBy2Ref(ref int x)
{
    x += 2;
}

void AddBy3(int x)
{
    x += 3;
}

void AddBy2(int x)
{
    x += 2;
}
#endregion

#region Lambda表达式：如果一个方法只调用一次，无需声明该方法，使用lambda表达式更简便。=>读作：goes to
//使用Lambda表达式重写AddBy2Ref和AddBy3Ref方法：
//DelWithRefPara cascadingAddRef = (ref int x) => x += 2 ; //AddBy2Ref
//cascadingAddRef += (ref int x) => { x += 3; }; //AddBy3Ref
//int a = 1;
//cascadingAddRef(ref a);
//Console.WriteLine(a);
#endregion

#region 使用Interface替代delegate
////delegate使用不当会造成内存泄露或性能下降，如果要实现协作，可以使用Interface替代。
////但interface不如delegate灵活，实际工作中，更多的是使用lambda表达式去实现委托。
////下面使用Interface重构ProductFactory
IProductFactory PizzFactory = new PizzaFactory();
IProductFactory ToyCarFactory = new ToyCarFactory();
WrapFactoryInterface WrapFactoryInterface = new WrapFactoryInterface();

Box box1 = WrapFactoryInterface.WrapProduct(PizzFactory);
Box box2 = WrapFactoryInterface.WrapProduct(ToyCarFactory);
Console.WriteLine($"{box1.Product.Name},\n{box2.Product.Name}");

///// <summary>
///// 通常Leader写接口，组员具体实现Leader写的接口即可。程序中对接口的定义，就是对程序功能的定义，但功能的具体实现，则由程序员完成。
///// </summary>
interface IProductFactory
{
    Product Make();
}

class PizzaFactory : IProductFactory
{
    public Product Make()
    {
        Product product = new Product();
        product.Name = "Cheese Pizza";
        product.Price = 30;
        return product;
    }
}

class ToyCarFactory : IProductFactory
{
    public Product Make()
    {
        Product product = new Product();
        product.Name = "Truck";
        product.Price = 100;
        return product;
    }
}

interface IWrapFactoryInterface
{
    public Box WrapProduct(IProductFactory productFactory);
}


class WrapFactoryInterface: IWrapFactoryInterface
{
    public Box WrapProduct(IProductFactory productFactory)
    {
        Box box = new Box();
        Product product = productFactory.Make();
        box.Product = product;
        return box;
    }
}
#endregion

class Calculator
{
    public void Version()
    {
        Console.WriteLine("Version: v1.01");
    }

    public void ShowName(string name)
    {
        Console.WriteLine($"Name: {name}");
    }

    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Sub(int a, int b)
    {
        return a - b;
    }
}

public delegate int CalcDelegate(int x, int y); //自定义一个委托类型，返回值为int，接受两个int作为参数。注意：委托本身也是一个类。
//函数签名

delegate void DelWithRefPara(ref int x);

class Logger
{
    public void Log(Product product)
    {
        Console.WriteLine($"Product {product.Name} created at {DateTime.UtcNow}, price is {product.Price}");
    }
}

class Product
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
}

class Box
{
    public Product Product { get; set; }
}

class WrapFactory
{
    /// <summary>
    /// 模板方法：Product product = getProduct();这行就是一个方法占位符，这里不关心这个product怎么获得，具体的实现方式，在调用WrapFactory.WrapProduct()时才确定。
    /// 这样写的好处是，WrapFactory.WrapProduct方法以后不需要再改变，如果以后有更多的product，只需要在ProductFactory中添加新的产生product的。
    /// 方法就可以了。
    /// 回调方法：logCallback没有返回值，根据逻辑判断是否调用该回调方法。
    /// 作者A写了WrapFactory.WrapProduct方法，作用是将一个Product对象封装成Box对象，但他并不关心这个Product对象是如何产生的。
    /// </summary>
    /// <param name="getProduct"></param>
    /// <returns></returns>
    public Box WrapProduct(Func<Product> getProduct, Action<Product> logCallback)
    {
        Box box = new Box();
        Product product = getProduct.Invoke();
        box.Product = product;
        //如果价格>50，再记录。
        if (product.Price >= 50)
        {
            logCallback(product);
        }
        return box;
    }
}

/// <summary>
/// 作者B要使用作者A写的WrapFactory.WrapProduct方法，就需要自己去写一个方法，去实现具体的方法来“生产”不同的product，作为参数传入WrapFactory.WrapProduct方法。
/// </summary>
class ProductFactory
{
    public Product MakePizza()
    {
        Product product = new Product();
        product.Name = "Cheese Pizza";
        product.Price = 30;
        return product;
    }

    public Product MakeToyCar()
    {
        Product product = new Product();
        product.Name = "Truck";
        product.Price = 100;
        return product;
    }
}

class Student
{
    public int Id { get; set; }
    public ConsoleColor PenColor { get; set; }

    public void DoHomeWork()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.ForegroundColor = this.PenColor;
            Console.WriteLine($"Student {Id} has been doing homework for {i} hours...");
            Thread.Sleep(1000);
        }
    }
}