//事件

#region 发布者订阅者模式：发布者定义一些列事件，订阅者订阅这些事件，并向发布者提供一些方法，当事件触发时，订阅者就会执行相应方法。基于委托。
//订阅者提供的方法被称为回调方法。
//A定义一个事件叫“闹铃响”，B订阅了该事件，并向该事件提供方法“起床”，当A触发“闹铃响”事件时，执行“起床”。
//A：发布者；B：订阅者；“闹铃响”：事件；“起床”：回调方法。
#endregion

#region 事件：对委托的封装。事件内部有一个私有的委托，程序员无法直接访问该委托，但可以通过接口注册调用列表，当该事件被触发时，其私有委托就会执行其调用列表。
//注意：事件是成员，和字段、属性、方法一样，只能声明在类或结构中。
//下面的例子中，incrementer在计数时，可以触发dozens的某个方法。
Incrementer incrementer = new Incrementer();
Dozens dozens = new Dozens(incrementer);
incrementer.DoCount();
Console.WriteLine($"Number of dozens: {dozens.DozensCount}");
#endregion

#region 在回调中传参
//调用传参版本的事件。
//incrementer.DoCountWithArgs();
//Console.WriteLine($"Number of dozens: {dozens.DozensCount}");
//incrementer.CountedADozenWithArgs -= dozens.IncrementDozensCountWithArgs; //将incrementer.CountedADozenWithArgs事件和回调解绑定。
//incrementer.DoCountWithArgs(); //再次触发incrementer.CountedADozenWithArgs事件，回调并不会执行，因此dozens.DozensCount属性依然是18。
//Console.WriteLine($"Number of dozens: {dozens.DozensCount}");
#endregion


/// <summary>
/// 发布者
/// </summary>
class Incrementer
{
    //EventHandler是.net内置委托，专门用来处理事件。事件必须是public，这样才能让订阅者将回调函数和事件绑定。
    public EventHandler? CountedADozen; //创建事件，?表示该事件的调用列表可能为空。EventHandler不会给回调函数传参。
    public EventHandler<IncrementerEventArgs>? CountedADozenWithArgs; //事件的泛型版本，其会给回调函数的第二个参数传入IncrementrEventArgs类型的参数。

    public void DoCount()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i % 12 == 0 && CountedADozen != null) //判断一下CountedADozen事件的调用列表是否为空。
            {
                CountedADozen(this, null); //触发事件时，使用EventHandler的参数。第二个参数为空，表示无需向回调函数传参。this表示事件的触发者是当前的Incrementer对象。注意：事件只能在类的内部被触发。
            }
        }
    }

    public void DoCountWithArgs()
    {
        IncrementerEventArgs args = new IncrementerEventArgs();
        for (int i = 0; i < 100; i++)
        {
            Thread.Sleep(100);
            if (i % 12 == 0 && CountedADozenWithArgs != null) //判断一下CountedADozen事件的调用列表是否为空。
            {
                args.IterationCount = i;
                args.CurrentTime = DateTime.Now;
                CountedADozenWithArgs(this, args); //触发事件时，将args传给回调函数。注意：事件只能在类的内部被触发。
            }
        }
    }
}

/// <summary>
/// 订阅者
/// </summary>
class Dozens 
{
    public int DozensCount {  get; private set; }

    public Dozens(Incrementer incrementer)
    {
        DozensCount = 0;
        incrementer.CountedADozen += IncrementDozensCount; //订阅者订阅CountedADozen事件，并将回调方法IncrementDozensCount绑定该事件。
        incrementer.CountedADozenWithArgs += IncrementDozensCountWithArgs; //订阅另一个传参版本的事件，将IncrementDozensCountWithArgs回调方法与之绑定。
    }

    void IncrementDozensCount(object sender, EventArgs e) //回调方法。这里的参数列表必须和EventHandler的参数列表一致。sender表示触发事件的对象，e不携带任何实际参数。
    {
        //Console.WriteLine(sender); //打印出来发现是incrementer对象。
        DozensCount++;
    }

    //将回调设置成public，在外部就可以随时与事件进行绑定和解绑。
    public void IncrementDozensCountWithArgs(object sender, IncrementerEventArgs e) //回调方法。注意：这里e的类型从EventArgs变成了IncrementrEventArgs，传入了实际的参数。
    {
        DozensCount++;
        Console.WriteLine($"{e.CurrentTime}: Iteration {e.IterationCount}"); //打印出事件触发者传入的参数。
    }
}

/// <summary>
/// 如果想在事件发生时给回调函数传入参数，需要自定义一个派生自EventArgs的类，其保存需要传入的参数。
/// 通常以发布者+EventArgs命名。
/// </summary>
public class IncrementerEventArgs : EventArgs
{
    public DateTime CurrentTime {  get; set; } //将被传入回调函数的第二个参数。
    public int IterationCount { get; set; } //将被传入回调函数的第二个参数。
}

