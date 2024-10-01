//异步 

#region 多线程初识
#region 多线程:线程是操作系统中能够独立运行的最小单位；线程是进程的一部分；main函数是一个进程的主线程入口，在主线程中，可以开启多个子线程。
//开启两个线程，对count变量自增，当两个线程执行完毕后，count是多少？
using MyUtilities;
using System.Diagnostics;

int count = 0;
//var thread1 = new Thread(ThreadMethod);
//var thread2 = new Thread(ThreadMethod);
//thread1.Start();
//thread2.Start();
//thread1.Join();
//thread2.Join();
//Console.WriteLine(count);

//void ThreadMethod()
//{
//    for (int i = 0; i < 10000; i++)
//        count++;
//}
//count的结果是不确定的，有时可能是20000，有时会是一个比20000更小的数。这是因为当两个线程同时对一个对象进行操作的时候，这个操作本身(count++)不是原子的。
//例如，Thread1拿到了count，此时值是1，在Threa1对count进行加1操作前，Thread2也拿到了count，此时count值也是1，然后Thread1和Thread2都对各自拿到的count
//执行count++，这时count的值变成了2而非3，这就是为什么最终的结果可能会小于20000。
//所谓原子化(atomic)的操作，表示这个操作要么被完全执行，要么完全不执行。count++不是一个原子化的操作，因为首先线程需要拿到count，然后对其+1，最后在把值赋
//给count，这个过程中，如果出现竞争(race condition，即多个线程抢夺对同一个对象的使用权)，就会导致count++的操作变得不完整。原子化的自增操作，需要保证任何
//时候，只有一个线程可以拿到对一个对象的使用权，在操作期间，其他线程只能等待，操作完毕后，其他线程才可以获得对这个对象的使用权。
#endregion

#region 原子操作和锁
//为了避免race condition，C#中的做法是：1.使用一些内置的原子操作；2.使用锁。
//常见的原子操作：Increment、Decrement、Add、Exchange、CompareExchange
//缺点是只能执行简单的操作。
//object LockObj = new object();
//var thread3 = new Thread(Increment);
//var thread4 = new Thread(Increment);
//thread3.Start();
//thread4.Start();
//thread3.Join();
//thread4.Join();
//Console.WriteLine(count);

//void Increment()
//{
//    for (int i = 0; i < 10000; i++)
//        Interlocked.Increment(ref count);
//}

////锁:任何线程执行count++前会拿到LockObj对象，当持有此对象时，lock (LockObj)作用域内的所有语句都是原子的，即里面的对象不会被其他线程操作，
////直到当前线程离开lock (LockObj)的作用域。
////锁的作用域内可以执行更复杂的操作。
//void LockIncrement()
//{
//    for (int i = 0; i < 10000; i++)
//    {
//        lock (LockObj)
//        {
//            count++;
//        }
//    }
//}
#endregion

#region Parallel & PLinq(Parallel Linq): 在C#中，如果需要用多线程遍历集合，可以使用Parallel或PLinq。
//HeavyJob每次执行至少需要100ms，对于20个元素的array，单线程执行需要至少20*100 = 2000ms。
//var inputs = Enumerable.Range(1, 20).ToArray();
//var outputs = new int[inputs.Length];

////单线程for循环:
//var sw = Stopwatch.StartNew();
//for (int i = 0; i < inputs.Length; i++)
//{
//    outputs[i] = HeavyJob(inputs[i]);
//}
//Console.WriteLine($"Single thread time elapsed: {sw.ElapsedMilliseconds}ms");

////Parallel:
//sw.Restart();
//Parallel.For(0, outputs.Length, i => outputs[i] = HeavyJob(inputs[i]));
//Console.WriteLine($"Parallel.For time elapsed: {sw.ElapsedMilliseconds}ms");

////PLinq:
//sw.Restart();
//outputs = inputs.AsParallel().AsOrdered().Select(x => HeavyJob(x)).ToArray();
//Console.WriteLine($"PLinq time elapsed: {sw.ElapsedMilliseconds}ms");

//outputs.Show();
////Parallel和PLinq方式可以替代new Thread方法，因为使用传统的新建一个线程的方式编程更加复杂，需要关注线程的生命周期、死锁等问题。
////但在某些时候，如果子线程中需要解决更复杂的问题时，还是需要手动开启子线程去完成。Parallel和PLinq仅支持多线程方式对集合的遍历。

//int HeavyJob(int input)
//{
//    Thread.Sleep(100);
//    return input * input;
//}
#endregion

#region 结束一个线程：thread.Join()和thread.Interrupt()；不要使用thread.Abort()，可能导致未知错误。
//thread.Join()等待子线程的停止，但会阻塞主线程，直到子线程结束，主线程才会恢复。即thread.Join()等待子线程“自然死亡”。
//子线程可接受object?类型的输入参数start。
//var subThread = new Thread(start: (object? input) =>
//{
//    for (int i = 0; i < 10; i++)
//    {
//        Thread.Sleep(200);
//        Console.WriteLine($"Subthread is running...{input}");
//    }
//    Console.WriteLine("Subthread is about to finish.");
//}){ IsBackground = true, Priority = ThreadPriority.Normal }; //对当前子线程的属性进行配置，设为后台线程，优先级设为normal。

//subThread.Start(123); //开启子线程。
//Console.WriteLine("Main thread: waiting for subthread to finish...");
//subThread.Join(); //主线程运行到这里，主线程会被阻塞住，等待子线程完全执行完毕，否则不会打印最后的Done。
//Console.WriteLine("Done");

//但有时候我们不知道一个子线程会运行多长时间才会结束，可能希望能够手动杀死子线程，这时可以使用thread.Interrupt()。
//var subThread2 = new Thread(start: (object? input) =>
//{
//    try //使用try catch去捕捉ThreadInterruptedException，捕捉到该异常后，子线程退出。
//    {
//        for (int i = 0; i < 10; i++)
//        {
//            Thread.Sleep(200);
//            Console.WriteLine($"Subthread is running...{input}");
//        }
//    }
//    catch (ThreadInterruptedException)
//    {
//        Console.WriteLine("Subthread has been interrupted!");
//    }
//    finally 
//    {
//        Console.WriteLine("Subthread is about to finish.");
//    }
//}){ IsBackground = true, Priority = ThreadPriority.Normal }; 

//subThread2.Start(123); //开启子线程。
//Console.WriteLine("Main thread: waiting for subthread to finish...");
//Thread.Sleep(500);
//subThread2.Interrupt(); //向子线程发出interrupt信号，同时继续向下执行，不会阻塞。
////subThread2.Join(); //如果需要保证Done一定在子线程结束之后才打印，这里可以使用Join去等待子线程完全结束。如果不用Join，则无法保证Done是在子线程结束
//                     //前打印还是在子线程结束之后打印。
//Console.WriteLine("Done");

//小技巧：
//子线程并不总是能保证捕捉到ThreadInterruptedException，如果子线程非常忙碌时间片占用率极高，此时可能发出了中断异常子线程也无法捕捉到。
//为了避免这个问题，在子线程中可以加入Thread.Sleep(0)去适当中断子线程的运行，从而在某个空隙可以捕捉到中断异常。
//var subThread3 = new Thread(start: (object? input) =>
//{
//    try //使用try catch去捕捉ThreadInterruptedException，捕捉到该异常后，子线程退出。
//    {
//        while (true)
//        {
//            //如果没有Thread.Sleep(0)，即使主线程发出了中断异常，子线程也没有资源去catch这个异常，因为子线程的时间片已经被while循环占满了。
//            //Thread.Sleep(0); 
//        }
//    }
//    catch (ThreadInterruptedException)
//    {
//        Console.WriteLine("Subthread has been interrupted!");
//    }
//    finally
//    {
//        Console.WriteLine("Subthread is about to finish.");
//    }
//}){ IsBackground = true, Priority = ThreadPriority.Normal }; 

//subThread3.Start(123); //开启子线程。
//Console.WriteLine("Main thread: waiting for subthread to finish...");
//Thread.Sleep(500);
//subThread3.Interrupt(); 
//subThread3.Join(); //如果子线程无法正确响应subThread3.Interrupt()，那么主线程会一直阻塞在这里无法结束。
//Console.WriteLine("Done");
#endregion

#region 在多线程编程中使用线程安全的数据结构: ConcurrentBag、ConcurrentDictionary、ConcurrentQueue、ConcurrentStack
//var queue = new Queue<int>(); //Queue是线程不安全的数据结构，在多线程编程中会出现未知错误。
//var queue = new ConcurrentQueue<int>(); //如果一个对象需要在多个线程中被使用，那么使用线程安全的数据结构，此时无需手动加锁等操作，也可以保证线程安全。
//var producer = new Thread(() =>
//{
//    for (int i = 0; i < 20; i++)
//    {
//        Thread.Sleep(100);
//        queue.Enqueue(i);
//    }
//});
//var consumer1 = new Thread(ReadNumber);
//var consumer2 = new Thread(ReadNumber);

//producer.Start();
//consumer1.Start();
//consumer2.Start();
//consumer1.Join();
//consumer2.Join();

//void ReadNumber()
//{
//	try
//	{
//		while (true)
//		{
//			if(queue.TryDequeue(out int temp))
//			{
//                Console.WriteLine(temp);
//			}
//			Thread.Sleep(100);
//		}
//	}
//	catch (ThreadInterruptedException)
//	{
//        Console.WriteLine("Consumer interrupted!");
//	}
//}
#endregion
#endregion

#region 异步
#region 概念
//异步是对线程池的封装，但也可以不使用线程池。异步编程并总不意味着多线程，单线程同样可以异步编程。
//多线程的特点：
//1.适用于长期的CPU密集型任务，比如开一个后台线程进行长期的计算。
//2.线程的开启和销毁开销很大，对于小型任务，开启一个线程的开销可能比任务本身的开销还要大。
//3.提供更底层的控制：锁、信号量等。
//4.不易于返回值，比如需要在主线程中阻塞，等待子线程完成拿到返回之后再继续主线程。
//5.代码书写比较繁琐，容易出现意想不到的错误(死锁等)，难以debug。

//异步的特点：
//1.适合IO密集型操作，例如读写文件。
//2.适合短小但反复重复的任务。
//3.可以避免阻塞主线程，提高系统的响应和并发能力。

//异步的流程：
//1.主线程开启一个或多个异步任务，系统自行给每个任务分配线程；
//2.每个任务执行的时候，主线程可选择等待(阻塞)这些任务的结束，也可以在任务执行的同时做别的事情(例如UI线程对用户鼠标拖拽等事件的响应)。
#endregion

#region Task<T>: C#中通常使用Task对异步方法进行包装。开始任务，等待任务，查看任务状态；T表示任务返回值为T类型，非泛型版本表示无返回值
//var task = new Task<string>(() =>
//{
//    Task.Delay(100);
//    return "done";
//});

//Console.WriteLine(task.Status);
//task.Start(); //Start()方法不会阻塞主线程，后续打印task.Status的方法会在task运行的同步执行。
////task.Wait(); //Wait()方法会阻塞主线程，直到task结束或触发异常，才继续执行Wait()后的代码。
//Console.WriteLine(task.Status);
//Thread.Sleep(1000);
//Console.WriteLine(task.Status);
//Thread.Sleep(1000);
//Console.WriteLine(task.Status);
//Console.WriteLine(task.Result);

//更常见的开启一个任务的方法是使用await Task.Run()，使用await关键字后，会直接返回原本的返回值，例如这里返回的是string，而不是Task<string>：

//Console.WriteLine($"[{Environment.CurrentManagedThreadId}]: 主线程");
//var task2Res = await Task.Run(() =>
//{
//    Console.WriteLine($"[{Environment.CurrentManagedThreadId}]: 任务线程"); //与主线程id不同
//    Task.Delay(100);
//    return "done";
//});
//Console.WriteLine($"[{Environment.CurrentManagedThreadId}]: 主线程"); //默认情况下会继续使用当前的线程，不会切回原线程。
//Console.WriteLine(task2Res);
#endregion

#region async void & async Task: async Task返回一个Task对象，对异步方法进行包装，使外部可以捕捉到其异常，或感知到其执行状态，使异步方法更安全
//如果一个异步方法没有返回值，那么可以写async void也可以写async Task；
//async void表示返回void，缺点是调用该异步方法的线程无法得知该异步方法的执行状态，因为返回值是void；也无法catch到里面抛出的异常，这是及其不安全的；
//async Task将该异步方法的信息进行了包装，可以得知该方法的状态信息，也可以使用try catch去捕捉异常；
//但在对一个事件的任务列表进行注册时，如果注册的方法是无返回值的异步方法，就只能使用async void去定义该方法。大多数应用场景在WPF或WinForm中。
//try
//{
//    //VoidAsync(); //无法捕捉异常；无法使用await关键字去等待该异步方法。
//    await TaskAsync(); //返回一个Task，就可以使用await去等待该异步方法；可以catch到该异步方法抛出的异常。
//    //TaskAsync().Wait(); //也可以使用Wait()，但推荐使用await关键字。
//    //TaskAsync().GetAwaiter().GetResult(); //不推荐。
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex);
//}

//async void VoidAsync()
//{
//    await Task.Delay(1000);
//    throw new Exception("Somthing was wrong!");
//}
//async Task TaskAsync()
//{
//    await Task.Delay(1000);
//    throw new Exception("Somthing was wrong!");
//}
#endregion

#region 同时执行多个异步任务： Wait & WaitAll & WaitAny，注意WaitAll、WaitAny、Wait(和Task.Result)会阻塞当前线程直到任务完成
//使用Wait()同步执行所有任务，每次执行一个任务时，都会阻塞主线程，因此虽然任务本身是异步的，但并没有节省时间。
Stopwatch watch = Stopwatch.StartNew();
//for (int i = 0; i <= 10; i++)
//{
//    var task = Task.Run(SearchAysnc);
//    //task.Wait(); //在for循环中，每个task都会将主线程阻塞在此，因此实际上这种调用是同步调用方式。
//    Console.WriteLine(task.Result); //task.Result本身也会阻塞主线程，直到子线程结束拿到Result后才返回给主线程打印，task.Result和Wait()功能相同。

//    //新的写法使用await task直接拿到真实的返回值string，而不是task.Wait()；同样阻塞主线程。
//    var res = await task;
//    Console.WriteLine(res);
//}
//Console.WriteLine($"{watch.ElapsedMilliseconds}ms in total.");

//使用WaitAll()并发执行所有任务，主线程阻塞在WaitAll()。
//WhenAll()会返回所有任务执行的结果，WaitAll()无返回值。如果任务有返回值，使用WhenAll()，返回的Task对象也可以更好的监测任务状态。
//List<Task<string>> tasks = new();
//for (int i = 0; i < 10; i++)
//{
//    tasks.Add(Task.Run(SearchAysnc));
//}
//watch.Restart();
////Task.WaitAll(tasks.ToArray()); //阻塞，但并发。
//var resArray = await Task.WhenAll(tasks); //或
////Task.WhenAll(tasks).Wait(); //或
//watch.Stop();
//foreach (var res in resArray)
//    Console.WriteLine(res);

//Console.WriteLine($"{watch.ElapsedMilliseconds}ms in total.");
//foreach (var task in tasks)
//    Console.WriteLine(task.Result);

//使用WaitAny()并发执行所有任务，主线程阻塞在WaitAny()，返回已完成任务的index。
//watch.Restart();
//Console.WriteLine(Task.WaitAny(tasks.ToArray())); //阻塞，但只要有1个任务结束，阻塞就会结束，继续执行后面的代码。
////Console.WriteLine(await Task.WhenAny(tasks.ToArray()).Result); //也可以使用await Task.WhenAny，直接拿到执行完毕的任务的真实结果。
//Console.WriteLine($"{watch.ElapsedMilliseconds}ms in total.");
//foreach (var task in tasks)
//    Console.WriteLine($"{task.Id}: {task.Status}");

//异步方法，返回一个Task<string>对象。
static async Task<string> SearchAysnc()
{
    Random rnd = new();
    Stopwatch watch = Stopwatch.StartNew();
    await Task.Delay(rnd.Next(3000)); //注意：不要在async方法里使用阻塞！！！这里使用Task.Delay，而不是Thread.Sleep因为await Task.Delay会释放主线程去执行别的工作，
                                      //不会阻塞主线程，而Thread.Sleep会阻塞主线程，这样一来异步任务就没有意义了。
                                      //Thread.Sleep(3000); //在console程序中，体现不出区别，如果是WPF或WinForm，Thread.Sleep会导致UI阻塞。因为这句之前的两行，依然是在UI线程中执行的
                                      //因此Thread.Sleep会使UI线程阻塞，只有在Thread.Sleep结束后，后面的两行代码才可能回到(也可能不回到，通过ConfigAwait(false)去配置，该配置在
                                      //Asp.net中无效，只在WinForm和WPF中生效。)UI线程去。而使用await执行耗时操作，可以确保await的任务使用新的线程执行，放开UI线程去
                                      //响应其他操作。（对于ASP.Net，则是放开web服务，相应其他用户的请求，而不会因为一个用户的好事请求阻塞整个web服务，从而提高并发量。）
    watch.Stop();
    return $"[{Environment.CurrentManagedThreadId}]: {watch.ElapsedMilliseconds}"; //语法糖：这里只需返回string，无需返回Task<string>。
}
#endregion

#region 使用CancellationToken取消任务：CancellationTokenSource实现了IDisposable接口，使用using语句或在finally中使用cts.Dispose()否则会内存泄漏
//所有的Async方法，都可以传入CancellationToken，建议在所有Async方法中，都传入这个参数。

//使用cts.Cancel()主动结束任务。
//using (CancellationTokenSource cts = new CancellationTokenSource())
//{
//var sw = Stopwatch.StartNew();
//var cancelTask = Task.Run(async () =>
//{
//    await Task.Delay(3000);
//    cts.Cancel(); //触发cts的Cancel行为。
//});

//try
//{
//    //下面的代码，运行两个任务，第一个任务在10秒后结束，传入了cts.Token；第二个任务会在3秒后触发cts.Cancel()，这个行为会被第一个任务捕捉到，因此第一个任务也会在3秒后结束。
//    //Task.WhenAll()会在全部任务结束后返回，因此该任务总计需要3秒左右，而非10秒。
//    //cts.Cancel()会触发TaskCanceledException，因此可以catch到该异常。
//    //常用的场景是，多个异步任务，使用不同方法进行计算，其中一个任务完成后其他任务即可停止无需继续下去，这时给每个任务都传入cts.Token，并且在结束后都执行cts.Cancel()，
//    //这样任意一个任务结束后，都会触发token的Cancel行为，其他任务都会被停止，无需浪费资源继续计算。
//    await Task.WhenAll(Task.Delay(10000, cts.Token), cancelTask);
//}
//catch (TaskCanceledException ex)
//{
//    Console.WriteLine(ex);
//}
//Console.WriteLine($"Task completed in {sw.ElapsedMilliseconds}ms.");
//}

//使用Delay参数，超时后自动取消任务。
//using (var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(3000)))
//{
//    var sw = Stopwatch.StartNew();
//    try
//    {
//        await Task.Delay(10000, cts.Token);
//    }
//    catch (TaskCanceledException ex)
//    {
//        Console.WriteLine(ex);
//    }
//    Console.WriteLine($"Task completed in {sw.ElapsedMilliseconds}ms.");
//}

//使用CancelAfter()方法定义超时时间。
//using (var cts = new CancellationTokenSource())
//{
//    cts.CancelAfter(3000);
//    var sw = Stopwatch.StartNew();
//    try
//    {
//        await Task.Delay(10000, cts.Token);
//    }
//    catch (TaskCanceledException ex)
//    {
//        Console.WriteLine(ex);
//    }
//    Console.WriteLine($"Task completed in {sw.ElapsedMilliseconds}ms.");
//}

#endregion

#region CancellationToken的小技巧
//建议在Async方法中尽量传入CancellationToken，但如果某个任务不希望被cancel掉，就不需要传入CancellationToken。
//这时有两个方法：
//1. 将Async方法包装一下，写一个需要CancellationToken版本，在这个版本中传入CancellationToken，再写一个不需要CancellationToken的版本，
//在这个版本中调用第一个版本，并传入CancellationToken.None即可；
//2. 将Async方法包装一下，传入一个可选参数CancellationToken，且默认值为null，在内部去判断是否传入了CancellationToken，如果没传入，那么
//调用Async方法时，传一个CancellationToken.None即可。
//class Demo
//{
//    /// <summary>
//    /// 需要CancellationToken的版本。
//    /// </summary>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    async Task FooAsync(CancellationToken cancellationToken)
//    {
//        await Task.Delay(1000);
//        var client = new HttpClient();
//        await client.GetStringAsync("/localhost:XXXX", cancellationToken);
//    }

//    /// <summary>
//    /// 不需要CancellationToken的版本。
//    /// </summary>
//    /// <returns></returns>
//    Task FooAsync() => FooAsync(CancellationToken.None);

//    /// <summary>
//    /// 第二种方案，传入可为空的cancellationToken，默认值为null。
//    /// </summary>
//    /// <param name="cancellationToken"></param>
//    /// <returns></returns>
//    async Task FooAsyncV2(CancellationToken? cancellationToken = null)
//    {
//        var token = cancellationToken ?? CancellationToken.None;
//        await Task.Delay(1000, token);
//    }
//}
#endregion

#region 自己写的方法中如何使用cancellationToken
//前面的演示中，Task.Delay()方法自带cancellationToken，传入后在触发了cts.cancel()后，Task.Delay()方法就会被终止，这是因为在方法内部对token的状态有一个判断。
//在自己写的方法中，就需要使用cancellationToken.IsCancellationRequested手动去判断token的状态。

//这里传入了一个超时3秒的cancellationToken。
//using (var cts = new CancellationTokenSource(3000))
//{
//    Foo foo = new Foo();
//    await foo.FooAsync(cts.Token);
//}
#endregion

#region 任务取消的对策
//1. 抛出异常；
//2. 返回一个task对象，其能够让外界追踪该task的状态；

//只演示第二种对策：
//using (var cts = new CancellationTokenSource())
//{
//    Foo2 foo2 = new Foo2();
//    int input = 1;
//    if (input % 2 == 0)
//        cts.Cancel();
//    Task<int> res = foo2.FooAsync(input, cts.Token); //这里不要使用await，否则会等待FooAsync执行完毕，后续if代码才执行；且会返回int而非Task；
//                                                     //且会抛出异常，但这里我们不希望使用try catch。
//由于没有使用await，因此FooAsync会在后台执行，且代码会继续向下执行，如果这里需要FooAsync的结果，那么使用res.Result调取结果，注意，如果此时任务已经
//结束，那么不会阻塞主线程，直接获取到结果；如果任务在调用res.Result时还没完成，那么会阻塞主线程直到获取到结果为止。
//    if (res.Status == TaskStatus.Canceled)
//        Console.WriteLine(res.Status);
//    else
//        Console.WriteLine(res.Result);
//}
#endregion

#region Task.Run可以直接传入cancellationToken，传入后无需再进行IsCancellationRequested的预检测，会自动抛出异常。但后后续代码无法自动触发TaskCanceledException异常
//在Task.Run中传入token的唯一作用就是替代任务一开始对IsCancellationRequested的预检测，一旦进入while循环，即使外部触发了cancel，那么Task也不会因此停止。
//因此如果是需要长期监测token状态，依然需要在每次循环中检测IsCancellationRequested。
//using (var cts = new CancellationTokenSource(2000))
//{
//    cts.Cancel();
//    try
//    {
//        await Task.Run(() =>
//        {
//            //if (cts.IsCancellationRequested) //Task.Run传入cts.Token的唯一作用就是替代这两句，一旦执行到while循环里，就无法捕捉外界对token的cancel操作了。
//            //    return;
//            while (true)
//            {
//                Thread.Sleep(1000);
//                Console.WriteLine("Foo...");
//            }
//        }, cts.Token);
//    }
//    catch (TaskCanceledException ex)
//    {
//        Console.WriteLine(ex);
//    }

//}
#endregion

#region token.Register()方法，可以在token被cancel后，执行一些后续方法
//using (var cts = new CancellationTokenSource(4000))
//{
//    cts.Token.Register(() => { Console.WriteLine("用户如果取消下载任务，那么可能需要在这里删除已下载内容。"); });
//    cts.Token.Register(() => { Console.WriteLine("在这里给用户弹窗显示一些信息。"); });
//    //注意：执行的顺序是，后注册的先被调用。
//    try
//    {
//        await Task.Run(() => 
//        {
//            int progress = 0;
//            while (true)
//            {
//                if (cts.Token.IsCancellationRequested)
//                    return;
//                Console.WriteLine($"正在下载...{progress++}%");
//                Thread.Sleep(1000);
//            }
//        }, cts.Token);
//    }
//    catch (TaskCanceledException ex)
//    {
//        Console.WriteLine(ex);
//    }
//}
#endregion

class Foo
{
    /// <summary>
    /// 异步方法，如果不被cancel，就一直执行HeayJob。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task FooAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                HeayJob();
            }

            //或用if判断，如果触发了cancel，就抛出异常。
            //while (true)
            //{
            //    if (cancellationToken.IsCancellationRequested)
            //        cancellationToken.ThrowIfCancellationRequested();
            //    HeayJob();
            //}      
        });
    }

    /// <summary>
    /// 耗时的同步方法。
    /// </summary>
    private void HeayJob()
    {
        Console.WriteLine("HeayJob is working...");
        Thread.Sleep(1000);
    }
}

class Foo2
{
    /// <summary>
    /// 如果不被cancel就执行HeayJob()，否则返回一个被cancel的Task对象。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> FooAsync(int input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<int>(cancellationToken);
        else return Task.Run( () => HeayJob(input));
        //注意：Task.Run接收一个Action或Func，但不能有显示的参数传入，例如Task.Run((int input) = >{})，只能是Task.Run((int input) = >{})
        //如果需要传参，封装一个函数然后将其返回给Lambda函数，例如这里的HeayJob(input)。
        //另一种传参的方法使用闭包技术：Lambda中的input，通过闭包从Task.Run外部传入。
        //else return Task.Run(() =>
        //{
        //    Thread.Sleep(1000);
        //    return input;
        //});
    }

    /// <summary>
    /// 耗时的同步方法。
    /// </summary>
    private int HeayJob(int input)
    {
        Thread.Sleep(1000);
        return input;
    }
}
#endregion
