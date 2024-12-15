//异步 

#region 多线程初识
#region 多线程:线程是操作系统中能够独立运行的最小单位；线程是进程的一部分；main函数是一个进程的主线程入口，在主线程中，可以开启多个子线程。
//开启两个线程，对count变量自增，当两个线程执行完毕后，count是多少？
using MyUtilities;
using Nito.AsyncEx;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Channels;
using ThirdPartyClassLib;

//int count = 0;
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

//锁:任何线程执行count++前会拿到LockObj对象，当持有此对象时，lock (LockObj)作用域内的所有语句都是原子的，即里面的对象不会被其他线程操作，
//直到当前线程离开lock (LockObj)的作用域。
//锁的作用域内可以执行更复杂的操作。
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
//    Thread.Sleep(100); //阻塞方法
//    return input * input;
//}
#endregion

#region 结束一个线程：thread.Join()和thread.Interrupt()；不要使用thread.Abort()，可能导致未知错误。
//thread.Join()等待子线程的停止，但会阻塞主线程，直到子线程结束，主线程才会恢复。即thread.Join()等待子线程“自然死亡”。
//子线程可接受object?类型的输入参数start。
//var subThread = new Thread((object? input) =>
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
//Console.WriteLine("Main thread: Subthread Done!");

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
//})
//{ IsBackground = true, Priority = ThreadPriority.Normal };

//subThread2.Start(123); //开启子线程。
//Console.WriteLine("Main thread: waiting for subthread to finish...");
//Thread.Sleep(500);
//subThread2.Interrupt(); //向子线程发出interrupt信号，同时继续向下执行，不会阻塞。
//subThread2.Join(); //如果需要保证Done一定在子线程结束之后才打印，这里可以使用Join去等待子线程完全结束。如果不用Join，则无法保证Done是在子线程结束
//                        //前打印还是在子线程结束之后打印。
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
//            Thread.Sleep(0); 
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
//})
//{ IsBackground = true, Priority = ThreadPriority.Normal };

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
//    try
//    {
//        while (true)
//        {
//            if (queue.TryDequeue(out int temp))
//            {
//                Console.WriteLine(temp);
//            }
//            Thread.Sleep(100);
//        }
//    }
//    catch (ThreadInterruptedException)
//    {
//        Console.WriteLine("Consumer interrupted!");
//    }
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
//Task<string> task = new Task<string>(() =>
//{
//    Task.Delay(100); //非阻塞的方法
//    return "done";
//});

//Console.WriteLine(task.Status);
//task.Start(); //Start()方法不会阻塞主线程，后续打印task.Status的方法会在task运行的同步执行。
//task.Wait(); //Wait()方法会阻塞主线程，直到task结束或触发异常，才继续执行Wait()后的代码。
//Console.WriteLine(task.Status);
//Thread.Sleep(1000);
//Console.WriteLine(task.Status);
//Thread.Sleep(1000);
//Console.WriteLine(task.Status);
//Console.WriteLine(task.Result);

//更常见的开启一个任务的方法是使用await Task.Run()，使用await关键字后，会直接返回原本的返回值，例如这里返回的是string，而不是Task<string>：
//使用await后，后续代码不会立即执行，而是等待await的任务完成，如果不想让主线程等待，那么不要使用await即可。
//Console.WriteLine($"[{Environment.CurrentManagedThreadId}]: 主线程");
//var task2Res = await Task.Run(async () =>
//{
//    Console.WriteLine($"[{Environment.CurrentManagedThreadId}]: 任务线程"); //这里可能与主线程id不同，但也可能相同。
//    await Task.Delay(1000);
//    Console.WriteLine($"[{Environment.CurrentManagedThreadId}]: 任务线程"); //与主线程id不同，也与262行不同，说明await语句产生了线程切换。
//    return "done";
//}).ConfigureAwait(false);
//Console.WriteLine($"[{Environment.CurrentManagedThreadId}]: 主线程"); //默认情况下会回到原线程。可以使用ConfigureAwait(false)，使其不回到原线程
////但在Console中由于不存在同步上下文的概念，因此此配置不起作用。
////在Console中默认会使用当前线程继续执行后续代码。
//Console.WriteLine(task2Res); //如果不写await，那么这里无法拿到task2Res正确的结果，因为没等任务执行完毕，代码就已经执行到这里了。这被称为Fire & Forget，一发既忘。
////                           //最好不要这样做。
#endregion

#region async void & async Task: async Task返回一个Task对象，对异步方法进行包装，使外部可以捕捉到其异常，或感知到其执行状态，使异步方法更安全
//如果一个异步方法没有返回值，那么可以写async void也可以写async Task；
//async void表示返回void，缺点是调用该异步方法的线程无法得知该异步方法的执行状态，因为返回值是void；也无法catch到里面抛出的异常，这是及其不安全的；
//async Task将该异步方法的信息进行了包装，可以得知该方法的状态信息，也可以使用try catch去捕捉异常；
//但在对一个事件的任务列表进行注册时，如果注册的方法是无返回值的异步方法，就只能使用async void去定义该方法。大多数应用场景在WPF或WinForm中。
//try
//{
//    //VoidAsync(); //无法捕捉异常；无法使用await关键字去等待该异步方法，所谓一发既忘，fire & forget。
//    await TaskAsync(); //返回一个Task，就可以使用await去等待该异步方法；可以catch到该异步方法抛出的异常。
//    //TaskAsync().Wait(); //也可以使用Wait()，阻塞，推荐使用await关键字。
//    //TaskAsync().GetAwaiter().GetResult(); //阻塞
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
//Stopwatch watch = Stopwatch.StartNew();
//for (int i = 0; i <= 10; i++)
//{
//    var task = Task.Run(SearchAysnc);
//    //task.Wait(); //在for循环中，每个task都会将主线程阻塞在此，因此实际上这种调用是同步调用方式。
//    //Console.WriteLine(task.Result); //task.Result本身也会阻塞主线程，直到子线程结束拿到Result后才返回给主线程打印，task.Result和Wait()功能相同。

//    //新的写法使用await task直接拿到真实的返回值string，而不是task.Wait()；同样阻塞主线程。
//    var res = await task;
//    Console.WriteLine(res);
//}
//Console.WriteLine($"{watch.ElapsedMilliseconds}ms in total.");

//使用WaitAll()并发执行所有任务，当前线程等待在WaitAll()。
//WhenAll()会返回所有任务执行的结果，WaitAll()无返回值。如果任务有返回值，使用WhenAll()，返回的Task对象也可以更好的监测任务状态。
//List<Task<string>> tasks = new();
//for (int i = 0; i < 10; i++)
//{
//    tasks.Add(Task.Run(SearchAysnc));
//}
//watch.Restart();
////Task.WaitAll(tasks.ToArray()); //阻塞，但并发。
////var resArray = await Task.WhenAll(tasks); //或
//Task.WhenAll(tasks).Wait(); //或
//watch.Stop();
////foreach (var res in resArray)
////    Console.WriteLine(res);

//Console.WriteLine($"{watch.ElapsedMilliseconds}ms in total.");
//foreach (var task in tasks)
//    Console.WriteLine(task.Result);

//使用WaitAny()并发执行所有任务，当前线程等待在WaitAny()，返回已完成任务的index。
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
                                      //因此Thread.Sleep会使UI线程阻塞，只有在Thread.Sleep结束后，后面的两行代码才会回到UI线程去(使用ConfigAwait(false)，表示后续代码将失去同步上下文
                                      //(synchronization context)，即不会使用UI线程执行后续操作：在ASP.Net中，意味着失去HttpContext；在WPF或WinForm中意味着失去UI操作权限，因为将不会
                                      //返回UI线程。而Console程序不存在同步上下文，所以ConfigAwait(false)在console中不生效。在UI程序或Asp.Net中，如果在同步方法中阻塞异步方法，可能会
                                      //导致死锁：例如在异步任务中调用UI资源去计算一个返回值，而UI线程中在wait()该异步任务（或使用.Result等待其返回值），此时UI线程被阻塞，无法给异步任务
                                      //提供计算所需的资源，就会产生死锁）。而使用await执行耗时操作，可以确保await的任务使用新的线程执行，放开UI线程去
                                      //响应其他操作。（对于ASP.Net，则是放开web服务，相应其他用户的请求，而不会因为一个用户的耗时请求阻塞整个web服务，从而提高并发量。）
    watch.Stop();
    return $"[{Environment.CurrentManagedThreadId}]: {watch.ElapsedMilliseconds}"; //语法糖：这里只需返回string，无需返回Task<string>。
}
#endregion

#region 使用CancellationToken取消任务：CancellationTokenSource实现了IDisposable接口，使用using语句或在finally中使用cts.Dispose()否则会内存泄漏
//所有的Async方法，都可以传入CancellationToken，建议在所有Async方法中，都传入这个参数。

//使用cts.Cancel()主动结束任务。
//using (CancellationTokenSource cts = new CancellationTokenSource())
//{
//    var sw = Stopwatch.StartNew();
//    var cancelTask = Task.Run(async () =>
//    {
//        await Task.Delay(3000);
//        cts.Cancel(); //触发cts的Cancel行为。
//    });

//    try
//    {
//        //下面的代码，运行两个任务，第一个任务在10秒后结束，传入了cts.Token；第二个任务会在3秒后触发cts.Cancel()，这个行为会被第一个任务捕捉到，因此第一个任务也会在3秒后结束。
//        //Task.WhenAll()会在全部任务结束后返回，因此该任务总计需要3秒左右，而非10秒。
//        //cts.Cancel()会触发TaskCanceledException，因此可以catch到该异常。
//        //常用的场景是，多个异步任务，使用不同方法进行计算，其中一个任务完成后其他任务即可停止无需继续下去，这时给每个任务都传入cts.Token，并且在结束后都执行cts.Cancel()，
//        //这样任意一个任务结束后，都会触发token的Cancel行为，其他任务都会被停止，无需浪费资源继续计算。
//        await Task.WhenAll(Task.Delay(10000, cts.Token), cancelTask);
//    }
//    catch (TaskCanceledException ex)
//    {
//        Console.WriteLine(ex);
//    }
//    Console.WriteLine($"Task completed in {sw.ElapsedMilliseconds}ms.");
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
//    cts.CancelAfter(TimeSpan.FromSeconds(3));
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
//    try
//    {
//        Foo foo = new Foo();
//        await foo.FooAsync(cts.Token);
//    }
//    catch (OperationCanceledException ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
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
//    Task<int> res = foo2.FooAsync(input, cts.Token);   //这里不要使用await，否则会等待FooAsync执行完毕，后续if代码才执行；且会返回int而非Task；
//                                                       //且会抛出异常，但这里我们不希望使用try catch。
//    //由于没有使用await，因此FooAsync会在后台执行，且代码会继续向下执行，如果这里需要FooAsync的结果，那么使用res.Result调取结果，注意，如果此时任务已经
//    //结束，那么不会阻塞主线程，直接获取到结果；如果任务在调用res.Result时还没完成，那么会阻塞主线程直到获取到结果为止。
//    if (res.Status == TaskStatus.Canceled)
//        Console.WriteLine($"if: {res.Status}");
//    else
//        Console.WriteLine($"else: {res.Result}");
//}
#endregion

#region Task.Run可以直接传入cancellationToken，传入后无需再进行IsCancellationRequested的预检测，会自动抛出异常。但后后续代码无法自动触发TaskCanceledException异常
//在Task.Run中传入token的唯一作用就是替代任务一开始对IsCancellationRequested的预检测，一旦进入while循环，即使外部触发了cancel，那么Task也不会因此停止。
//因此如果是需要长期监测token状态，依然需要在每次循环中检测IsCancellationRequested。
//using (var cts = new CancellationTokenSource(4000))
//{
//    //cts.Cancel();
//    try
//    {
//        await Task.Run(() =>
//        {
//            //if (cts.IsCancellationRequested) //Task.Run传入cts.Token的唯一作用就是替代这两句，一旦执行到while循环里，就无法捕捉外界对token的cancel操作了。
//            //    return;
//            while (true)
//            {
//                //if(cts.IsCancellationRequested)
//                //    return;
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
//        });
//    }
//    catch (TaskCanceledException ex)
//    {
//        Console.WriteLine(ex);
//    }
//}
#endregion

#region 超时机制
//Thread多线程的超时机制.
//Join()方法接收一个超时参数，在超时后，检查线程是否停止，如果已停止返回true，否则返回false。
//如果2s后依然没有停止，调用Interrupt()停止线程。在函数内部，捕捉ThreadInterruptedException，并进行资源释放等操作。
//var thread = new Thread(Foo);
//thread.Start();
//if (!thread.Join(2000))
//{
//    thread.Interrupt();
//}
//Console.WriteLine("Done");

//Async Task异步的超时机制：假设fooTask需要3秒执行完毕，将fooTask和一个2s的delay一起放到whenany里，2s后delay任务执行完毕，返回该任务，
//比较completedTask是否是fooTask，如果不是说明已超时，这时调用cts.cancel主动取消fooTask，然后再等待其结束即可。
//这个过程中，fooTask的最后一句Foo end...无法被打印出来，因为在2s的时候任务就被cancel了。
//var cts = new CancellationTokenSource();
//var fooTask = FooAsync(cts.Token);
//var completedTask = await Task.WhenAny(fooTask, Task.Delay(2000));
//if (completedTask != fooTask)
//{
//    cts.Cancel();
//    await fooTask;
//    Console.WriteLine("Timeout, fooTask canceled");
//}

//使用扩展方法实现Task的timeout机制：.Net6以前需要自己写扩展方法。
//var cts = new CancellationTokenSource();
//var fooTask = FooAsync(cts.Token);
//try
//{
//    await fooTask.TimeoutAfter(TimeSpan.FromMilliseconds(2000));
//    Console.WriteLine("Success!");
//}
//catch (TimeoutException)
//{
//    cts.Cancel();
//    Console.WriteLine("Timeout!");
//}
//finally
//{
//    cts.Dispose();
//}
//Console.WriteLine("Done");

//使用WaitAsync()，默认接收一个timeout：.Net6以后可用。推荐使用。
//try
//{
//    await fooTask.WaitAsync(TimeSpan.FromMilliseconds(2000)); 
//    Console.WriteLine("Success!");
//}
//catch (TimeoutException)
//{
//    cts.Cancel();
//    Console.WriteLine("Timeout!");
//}
//finally
//{
//    cts.Dispose();
//}
//Console.WriteLine("Done");

void Foo()
{
    try
    {
        Console.WriteLine("Foo start...");
        Thread.Sleep(3000);
        Console.WriteLine("Foo end...");
    }
    catch (ThreadInterruptedException)
    {
        Console.WriteLine("Foo interrupted...");
    }
}

async Task FooAsync(CancellationToken token)
{
    try
    {
        Console.WriteLine("Foo start...");
        await Task.Delay(3000, token);
        Console.WriteLine("Foo end...");
    }
    catch (TaskCanceledException)
    {
        Console.WriteLine("Foo canceled...");
    }
}
#endregion

#region 生产者/消费者模式
//使用Thread实现生产者消费者队列：
//BlockingCollection是内置线程安全集合，可以接受另一个线程安全的集合作为underlying storage，这里使用ConcurrentQueue。
//设计了1个生产者，2个消费者；生产者每10ms生产一个元素，消费者每1s消费一个元素。
//using (var queue = new BlockingCollection<Message>(new ConcurrentQueue<Message>()))
//{
//    var sender = new Thread(SendMsg);
//    var receiver1 = new Thread(ReceiveMsg);
//    var receiver2 = new Thread(ReceiveMsg);
//    sender.Start("Sender");
//    receiver1.Start("Receiver1");
//    receiver2.Start("Receiver2");

//    sender.Join();
//    Thread.Sleep(1000);
//    receiver1.Interrupt(); //1s后杀死receiver
//    receiver2.Interrupt(); //1s后杀死receiver
//    receiver1.Join();
//    receiver2.Join();
//    Console.WriteLine();
//    //在receiver结束后，打印出集合里的元素，会发现被receivers take的元素不在集合中，说明被“消费”掉了。
//    foreach (var message in queue)
//        Console.WriteLine(message.Msg);
//    //这套代码并没有使用锁之类的底层的设计，因为我们使用的BlockingCollection、ConcurrentQueue本身就是线程安全的数据类型，
//    //在编程中，推荐使用内置的线程安全的数据结构，而不是自己造轮子，使用锁去设计自己的线程安全类。

//    //Add msg to queue
//    //WorkerName由Thread.Start()传入，Start方法只能接收object?类型的参数。
//    void SendMsg(object? WorkerName)
//    {
//        string threadName = (string)WorkerName!;
//        for (int i = 0; i <= 20; i++)
//        {
//            queue.Add(new Message(threadName, i.ToString()));
//            Console.WriteLine($"{threadName}, sent {i}");
//            Thread.Sleep(10);
//        }
//    }

//    void ReceiveMsg(object? WorkerName)
//    {
//        try
//        {
//            while (true)
//            {
//                //Take是一个阻塞方法。
//                //注意：当queue中有数据时，take会从queue里remove一个元素并返回；如果queue里没有可用元素，则会阻塞，直到有可用元素。
//                var msg = queue.Take();
//                Console.WriteLine($"{WorkerName} received {msg.Msg} from {msg.ThreadName}");
//                Thread.Sleep(1000);
//            }
//        }
//        catch (ThreadInterruptedException)
//        {
//            Console.WriteLine($"Thread {WorkerName} interrupted!");
//        }
//    }
//}

//使用Channel以异步形式实现生产者消费者队列：
//由于Take是一个阻塞方法，以上方案只能用在Thread多线程编程中，但异步编程要求是“不阻塞”。
//option定义一些channel的属性：
var option = new BoundedChannelOptions(20) //消息队列最多20个待处理的消息。
{
    FullMode = BoundedChannelFullMode.Wait, //当队列已满，新的消息会等待，直到队列有空位。
    SingleWriter = false, //只有1个生产者
    SingleReader = false, //可以有多个消费者
    AllowSynchronousContinuations = false, //允许消费者以同步方式处理数据，通常设为false：一般当消费者的处理速度大于生产者时，生产出一个元素后，再使用信号量去
                                           //通知消费者开销较大，这时使用同步的方式调用消费者性能更高，但这种情况较少出现。
};
//只能使用静态方法去生成Bounded或Unbounded channel。
//var channel = Channel.CreateBounded<Message>(option);
//var sender1 = SendMsgAsync(channel.Writer, "Sender1");
//var sender2 = SendMsgAsync(channel.Writer, "Sender2");
//var receiver1 = ReceiveMsgAsync(channel.Reader, "Receiver1");
//var receiver2 = ReceiveMsgAsync(channel.Reader, "Receiver2");

//await Task.WhenAll(sender1, sender2); //等待所有生产者生产完毕
//channel.Writer.Complete(); //当所有senders都执行完毕，writer发出信号表示已经不会有数据进入channel。
//                           //Reader在消费完毕后触发ChannelClosedException，而不是writer.complete()时就抛出异常。
//await Task.WhenAll(receiver1, receiver2);

async Task SendMsgAsync(ChannelWriter<Message> writer, string SenderName)
{
    for (int i = 0; i <= 20; i++)
    {
        await Task.Delay(100);
        await writer.WriteAsync(new Message(SenderName, i.ToString()));
        Console.WriteLine($"{SenderName} sent {i.ToString()}");
    }
}

async Task ReceiveMsgAsync(ChannelReader<Message> reader, string ReceiverName)
{
    //.Net8以前的Receiver写法：
    //try
    //{
    //    //注意：这里是reader去判断complete，当writer set complete时，如果channel里仍然有消息，那么reader依然会继续处理消息，直到channel为空
    //    //reader才会自动set complete，然后触发ChannelClosedException。
    //    while (!reader.Completion.IsCompleted)
    //    {
    //        await Task.Delay(500);
    //        var msg = await reader.ReadAsync(); //等同于多线程中BlockingCollection.Take方法，当Channel中有数据的时候就会异步地读取数据，区别是不会阻塞线程。
    //        Console.WriteLine($"{ReceiverName} received {msg.Msg} from {msg.ThreadName}");
    //    }
    //}
    //catch (ChannelClosedException)
    //{
    //    Console.WriteLine($"Channel closed!");
    //}

    //.Net8之后的Receiver写法：
    //无需处理ChannelClosedException了。
    await foreach (var msg in reader.ReadAllAsync())
    {
        Console.WriteLine($"{ReceiverName} received {msg.Msg} from {msg.ThreadName}");
    }
}
#endregion

#region Task.Result 和 Task.GetAwaiter().GetResult()/await的区别
//using (var cts = new CancellationTokenSource())
//{
//    var foo = new Foo2();
//    try
//    {
//        //Recall：Task.Result是一个阻塞方法，等待异步方法返回结果。
//        //但如果异步方法抛出异常，这里捕捉到的并不是异步方法里抛出的那个异常，而是一个AggregateException，异步方法里的那个异常，被包装在了InnerException里。
//        //var res = foo.FooAsync2(10, cts.Token).Result;

//        //通过Task.GetAwaiter().GetResult()则能直接拿到InnerException。
//        //var res = foo.FooAsync2(10, cts.Token).GetAwaiter().GetResult();

//        //或直接await
//        var res = await foo.FooAsync2(10, cts.Token);
//        Console.WriteLine(res);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex);
//    }
//}
#endregion

#region 如何在同步方法里调用异步方法：构造函数中调用IO密集操作
//想在一个同步方法中，调用一个异步方法会碰到以下问题：
//1. 在同步方法中，使用await调用一个异步方法会导致async的传染，该同步方法必须被标记为async，也就变成了异步方法；
//2. 使用Task.Result或Task.GetAwaiter().GetResult()，虽然不用await关键字不会导致异步的传染，但会导致同步方法的阻塞;
//3. 使用Fire & Forget，即直接调用异步方法，不使用await：外界无法catch到异步方法抛出的异常，也无法追踪该任务的状态。

//方法一和方法二：
//try
//{
//    var dataModel = new MyDataModel();
//    Console.WriteLine("Loading data in constructor...");
//    while (true)
//    {
//        Thread.Sleep(1);
//        if (dataModel.IsDataLoaded)
//            break;
//    }
//    var data = dataModel.Data;
//    data.Show();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex);
//}

//方法三：
//try
//{
//    var dataModel = await MyDataModel.CreateAsync();
//    Console.WriteLine("Loading data in constructor...");
//    while (true)
//    {
//        Thread.Sleep(1);
//        if (dataModel.IsDataLoaded)
//            break;
//    }
//    var data = dataModel.Data;
//    data.Show();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex);
//}

//总结：
//如果接受异步的传染性，整个项目都可以被改为异步形式，而C#内置了非常多的异步方法，因此大多数时候，不需要担心异步的传染性，
//如果非要在同步方法中调用异步，那么会比较麻烦，推荐方案一。
#endregion

#region 如何在异步方法中实现同步机制：通过加锁
//所谓同步，就是多个任务之间是顺序执行，而非同时执行。在异步任务中实现同步的需求，往往和多线程处理同一个资源相关。
//在多线程中的同步需求通过锁(lock、mutex、semephore、monitor等)实现；而异步中不能使用以上锁，因为他们都会导致阻塞，这与异步的思路不一致。

//无法在lock语句中使用await：见CanNotUseLockInAsyncTask类。

//1. 异步任务
//var sw = new Stopwatch();
//CanNotUseLockInAsyncTask AsyncLock = new();
//var tasks = Enumerable.Range(1, 10).Select(x => AsyncLock.HeayJobAsync(x)); //这个版本未上锁，10个任务异步执行，共计约100ms
//sw.Restart();
//var res1 = await Task.WhenAll(tasks);
//Console.WriteLine(string.Join(",", res1));
//Console.WriteLine(sw.ElapsedMilliseconds);

////2. 通过第三方库Nito.asyncex可在lock中运行异步任务实现异步的同步机制：
//var tasksLock = Enumerable.Range(1, 10).Select(x => AsyncLock.HeayJobLockAsync(x)); //这个版本由于上了锁，锁内的资源一次只能被一个线程操作
//                                                                                    //其余线程等待该线程操作完毕后才能接过锁然后在操作锁内的资源
//                                                                                    //导致10个任务无法异步执行，因此共计约10*100ms。
//sw.Start();
//var res2 = await Task.WhenAll(tasksLock);
//Console.WriteLine(string.Join(",", res2));
//Console.WriteLine(sw.ElapsedMilliseconds);
//sw.Stop();

////3. C#内置SemaphoreSlim，可通过调整参数，灵活配置所需的线程个数：
//var tasksSemaphoreSlim = Enumerable.Range(1, 10).Select(x => AsyncLock.HeayJobSemaphoreSlim(x)); //取决于SemaphoreSlim的配置，当参数为(1,1)时，
//                                                                                                 //和HeayJobLockAsync时间相同。
//sw.Restart();
//var res3 = await Task.WhenAll(tasksSemaphoreSlim);
//Console.WriteLine(string.Join(",", res3));
//Console.WriteLine(sw.ElapsedMilliseconds);
//sw.Stop();
#endregion

#region 如何在异步方法中实现同步机制：通过信号量
////1. 使用Nito.AsyncEx中提供的AsyncAutoResetEvent
//var signal = new AsyncAutoResetEvent(false);
////2秒后，signal.Set()发出信号。
//var setter = Task.Run(() =>
//{
//    Thread.Sleep(2000);
//    signal.Set();
//    Console.WriteLine("Signal Set!");
//});

//////signal.WaitAsync()会在接收到set信号前阻塞，直到接收到set信号后，才执行后面的内容。
//var waiter = Task.Run(async () =>
//{
//    await signal.WaitAsync();
//    Console.WriteLine("Signal Received!");
//});
//////虽然WhenAll接受两个异步任务，但第二个任务实际上在第一个任务发出信号后才执行，因此实际上是同步执行。
//await Task.WhenAll(setter, waiter);

//2. 使用TaskCompletionSource(泛型表示需要在任务间传递的信息)，不仅可以实现异步任务的同步，也可以在不同异步任务之间传递信息。
//var tcs = new TaskCompletionSource<string>();
////2秒后，将setter.TaskStatus设置为RanToCompletion，且将一个字符串传递出去。
//var setter = Task.Run(() =>
//{
//    Thread.Sleep(2000);
//    tcs.SetResult("Setter completed!"); //由于我们声明的泛型是string，因此可以传递string。
//    //同样也可以将tcs的状态设置为cancel、exception等。
//    //SetResult只能执行一次，再次执行会抛异常。
//    if (!tcs.TrySetResult("Setter completed"))
//    {
//        Console.WriteLine("Do not set twice!");
//    }
//});

//////tcs.Task是tcs内部的一个属性，当tcs的Task属性被设置为RanToCompletion后，await tcs.Task就执行完毕了，接着可以执行后续代码，从而实现异步任务的同步机制。
//var waiter = Task.Run(async () =>
//{
//    var setterInfo = await tcs.Task;
//    Console.WriteLine($"Setter info received: {setterInfo}");
//});
//await Task.WhenAll(setter, waiter);
//通过TaskCompletionSource，可以实现多个异步任务的同步执行，且可以在任务之间传递数据。
#endregion

#region 总结：在异步中实现同步机制
//1. 传统的Thread很难实现不同线程之间的通信，但Task可以很容易实现这一点，从而实现异步任务的同步执行。
//2. 传统的Thread中的锁，无法在Task中直接使用，需要借助第三方库或SemaphoreSlim。
//3. 想要在异步中实现同步有两种方案，第一种是使用异步锁，第二种是使用信号量。
#endregion

#region ValueTask的使用
//一个常见的异步操作场景：首先在缓存中查看所需的数据是否存在，如果存在，直接返回缓存中的值；如果不存在，执行异步任务读取数据库里的值并返回。
//在这个场景中，如果缓存命中，那么实际上只返回一个值即可，无需返回一个Task（相比直接返回结果，Task具有更大的内存开销），但如果缓存命中失败，就需要调用异步任务返回一个Task。
//var ValueTaskDemo = new ValueTaskDemo();
//var res = await ValueTaskDemo.GetAsync(2); //首先去cache里获取key=2；cache未命中，执行异步操作，并将key=2添加到cache
//Console.WriteLine(res);
//ValueTaskDemo.ShowCache(); //这时发现key=2已经被添加到Cache中

////以上场景，推荐使用ValueTask<T>作为返回值。
//var res = await ValueTaskDemo.GetValueTaskAsync(3);
////注意：不要阻塞ValueTask，也就是说，不要对一个ValueTask对象使用.Result、.Wait()、.GetAwaiter().GetResult()
////如果一定要阻塞，那么先使用IsCompleted判断任务状态，在完成时才使用以上阻塞的方式获取结果。例如：
//var task = ValueTaskDemo.GetValueTaskAsync(3);
//if (task.IsCompleted)
//{
//    Console.WriteLine(task.Result);
//}
//Console.WriteLine(res);
//ValueTaskDemo.ShowCache(); //key=3被添加到Cache中
#endregion

#region 使用IProgress进行进度汇报：见Ch1_L12_Supplymentary，使用winform项目进行演示
//异步任务的执行过程中，对外界进行进度汇报。
#endregion

#region 如何在异步任务中调用并取消一个长时间运行的同步方法：Thread+Task

//方案1.使用一个超时任务，和LongRunningJob一起调用，期待当超时任务结束时，LongRunningJob也一起被取消掉：
//var start = Stopwatch.GetTimestamp();
//var task1 = Task.Run(LongRunningJob);
//var task2 = Task.Delay(1000);
//await Task.WhenAny(task1, task2);
//var elapsed = Stopwatch.GetElapsedTime(start);
//Console.WriteLine($"Elapsed:{elapsed}");
//Console.WriteLine("Done");
//Console.ReadKey(); //发现：1秒后“Done”被打印出来，3秒后"Long running job completed!"被打印出来
//这表示，task2并没有因为task1结束而结束，WhenAny并没有取消LongRunningJob，LongRunningJob依然在后台执行。

//假设这是一个无法修改的耗时方法，比如从其他库中调用的函数。
static void LongRunningJob()
{
    Thread.Sleep(3000);
    Console.WriteLine("Long running job completed!");
}

//方案2.使用Thread来运行LongRunningJob，使用Thread.Interrupt强制打断，并使用信号量(TaskCompletionSource)等方式暴露一个可等待的异步任务：
//using (var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(2000)))
//{
//    try
//    {
//        var task = new CancelableThreadTask(LongRunningJob);
//        await task.RunAsync(cts.Token);
//    }
//    catch (TaskCanceledException)
//    {
//        Console.WriteLine("Task was canceled");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Task failed:{ex}");
//    }
//    Console.WriteLine("Done"); //发现：2秒后打印Task was canceled和Done；继续等待并没有打印Long running job completed!，
////    说明LongRunningJob的确被取消了。这是由于当cts被cancel时，_thread.Interrupt()被触发，运行委托的线程被
////    杀死，因此委托本身也就不复存在，所以任务可以被正确取消。
//    Console.ReadKey();
//}

//CancelableThreadTask的局限
//见Interrupting threads Notes from https://learn.microsoft.com/en-us/dotnet/standard/threading/pausing-and-resuming-threads
//If the target thread is not blocked when Thread.Interrupt is called, the thread is not interrupted until it blocks.
//If the thread never blocks, it could complete without ever being interrupted.
//使用Thread.Interrupt打断线程时，如果目标线程里没有任何阻塞，那么Thread.Interrupt将不会生效，而是会继续执行。
//也就是说，如果目标方法里没有任何阻塞线程的操作，例如thread.sleep, thread.join等操作，那么Thread.Interrupt并不会生效。

//调用ThirdPartyUtils.CancelableSyncMethod()
//在CancelableSyncMethod中，for循环里是thread.sleep，因此这个方法可以被interrupt，CancelableThreadTask可以正常生效。
//using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
//var threadTask = new CancelableThreadTask(() => ThirdPartyUtils.CancelableSyncMethod());
//using (new SimpleTimer("Task started!"))
//{
//    try
//    {
//        await threadTask.RunAsync(cts.Token); //3s后ThirdPartyUtils.CancelableSyncMethod()被成功cancel
//    }
//    catch (OperationCanceledException)
//    {
//        Console.WriteLine("Task cancelled!");
//    }
//}
//Console.ReadKey();

//调用ThirdPartyUtils.UncancelableSyncMethod()
//在UncancelableSyncMethod中并没有任何线程阻塞的操作，导致thread.interrupt无法生效，因此CancelableThreadTask也无法正常生效。
//using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
//var threadTask = new CancelableThreadTask(() => ThirdPartyUtils.UncancelableSyncMethod());
//using (new SimpleTimer("Task started!"))
//{
//    try
//    {
//        await threadTask.RunAsync(cts.Token); //6s后ThirdPartyUtils.UncancelableSyncMethod()执行完毕，未能被成功cancel
//    }
//    catch (OperationCanceledException)
//    {
//        Console.WriteLine("Task cancelled!");
//    }
//}
//Console.ReadKey();

//后面探讨针对类似UncancelableSyncMethod这样的第三方库，如何打断其运行：
//从 https://learn.microsoft.com/en-us/dotnet/standard/threading/destroying-threads 我们得知，官方推荐的方法是：
//对于.net framework使用thread.abort，但对于.net5+的版本，在独立的进程中调用第三方方法，然后使用Process.kill杀死进程。
//The Thread.Abort method is not supported in .NET 5 (including .NET Core) and later versions.
//If you need to terminate the execution of third-party code forcibly in .NET 5+, run it in the separate process and use Process.Kill.
//我们使用process运行第三方库中的方法：
//using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
////注意：.\ThirdPartyMethodWrapper.exe的路径必须提前build才会生成
//var processTask = new CancelableProcessTask(@".\ThirdPartyMethodWrapper.exe", "uncancelable");
//using (new SimpleTimer("Task started!"))
//{
//    try
//    {
//        await processTask.RunAsync(cts.Token); //这次发现该方法在3秒后被正确的cancel掉了
//        Console.WriteLine("Task ended successfully!");
//    }
//    catch
//    {
//        Console.WriteLine("Task cancelled or failed!");
//    }
//}
//Console.ReadLine();
#endregion

#region 使用Barrier实现多个异步任务同时完成
//场景：2个任务同时进行，且对于每个任务来说，都有分为3个阶段，任意一个任务每完成一个阶段，都需要等待另一个任务也完成该阶段后，再同时开始下一个阶段，直到
//最后一个阶段同步完成。
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1000));
Barrier barrier = new Barrier(2, postPhaseAction =>
{
    Console.WriteLine($"阶段{postPhaseAction.CurrentPhaseNumber + 1}完成 \n");
});

var task1 = Task.Run(() =>
{
   for (int i = 0; i < 3; i++)
   {
       Console.WriteLine($"任务1正在进行阶段{i + 1}...");
       Thread.Sleep(2000);
       Console.WriteLine($"任务1完成阶段{i + 1}...");
       barrier.SignalAndWait(cts.Token); //指示本任务的某个phase已完成，等待其他任务；可接收一个cts以取消任务
   }
});

var task2 = Task.Run(() =>
{
    for (int i = 0; i < 3; i++)
    {
        Console.WriteLine($"任务2正在进行阶段{i + 1}...");
        Thread.Sleep(1000);
        Console.WriteLine($"任务2完成阶段{i + 1}...");
        barrier.SignalAndWait(cts.Token); //指示本任务的某个phase已完成，等待其他任务；可接收一个cts以取消任务
    }
});

var allTasks = Task.WhenAll([task1, task2]);
await allTasks;
if (allTasks.IsCompleted)
    Console.WriteLine("All tasks completed!");

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
            //while (!cancellationToken.IsCancellationRequested)
            //{
            //    HeayJob();
            //}

            //或用if判断，如果触发了cancel，就抛出异常。
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested(); //ctrl+alt+E搜OperationCanceledException，找到CLR里的这个异常，打勾后碰到该异常就会break，为了正常运行或debug，取消这个勾
                HeayJob();
            }
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
    /// 如果不被cancel就执行HeayJob()，否则返回一个被cancel的Task对象，可追踪该Task对象的状态是被cancel的。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> FooAsync(int input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<int>(cancellationToken);
        else return Task.Run(() => HeayJob(input));
        //注意：Task.Run接收一个Action或Func，但不能有显示的参数传入，例如Task.Run((int input) = >{})，只能是Task.Run(() = >{})
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
        Thread.Sleep(10000);
        return input;
    }

    /// <summary>
    /// 假设在异步方法中会抛出一个异常。
    /// </summary>
    /// <param name="input"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<int> FooAsync2(int input, CancellationToken token)
    {
        await Task.Delay(1000);
        throw new Exception("Error!");
        return input;
    }
}

class MyDataModel
{
    public IEnumerable<int> Data { get; set; }
    public bool IsDataLoaded { get; private set; } = false;

    /// <summary>
    /// 我们希望在同步的构造函数中，调用异步方法来初始化<see cref="MyDataModel.Data">Data</see>属性。
    /// 注意：方法一的扩展方法<see cref="AsyncExtension.Await(Task, Action?, Action{Exception}?)">Await()</see>的实现。
    /// </summary>
    public MyDataModel()
    {
        //LoadDataAsync(); //F&F会导致无法对任务进行追踪，也无法抓住内部的异常。

        //方法一：SafeFireAndForget
        //SafeFireAndForgetAsync(LoadDataAsync(), () => IsDataLoaded = true, ex => throw ex);

        //方法一的扩展方法
        //LoadDataAsync().Await(() => IsDataLoaded = true, ex => throw ex);

        //方法二：使用内置的ContinueWith()
        //LoadDataAsync().ContinueWith(t=>IsDataLoaded = true, TaskContinuationOptions.OnlyOnRanToCompletion); //只在Task成功执行后才执行回调。
        //这种调用只能传入成功的回调，如果想要再传入失败的回调，可以将两个回调包装成一个方法，将这个方法传入ContinueWith：
        //LoadDataAsync().ContinueWith(t => OnDataLoaded(t));
        //方法二的缺点：
        //1.ContinueWith()会返回一个Task，即使LoadDataAsync()本身并不需要返回任何东西，这凭空增加了一些开销；
        //2.ContinueWith()默认调用当前线程去执行后续操作，这一点是比较危险的。（回忆使用.Result后再调用UI线程执行一些操作时发生的死锁：.Result阻塞了UI线程，而在
        //Task里又需要UI权限去执行某些操作从而获得Result）

        //方法三：使用工厂函数调用私有的constructor，然后在工厂函数中await异步方法并赋值，最后返回实例。
        //缺点是：无法将这种形式创建的实例注册给IOC容器。
    }

    /// <summary>
    /// 模拟一个速度很慢的IO过程，例如从数据库/文件中读取一组数。
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    async Task LoadDataAsync()
    {
        await Task.Delay(1000);
        Data = Enumerable.Range(1, 10).ToList();
        throw new Exception("Data loading error!"); //模拟读取数据时发生异常。
    }

    /// <summary>
    /// 方法一：写一个async void方法包装需要调用的异步方法task，并传入成功和失败的回调。
    /// 在SafeFireAndForget中await该异步方法，就可以catch到其内部抛出的异常，然后
    /// 分别执行成功和失败回调即可。
    /// 注意：虽然外部无法捕捉到Async void (即SafeFireAndForget)方法内部抛出的异常，但是对于异步方法内部抛出的异常我们可以在SafeFireAndForget内部去处理，因为在
    /// SafeFireAndForget内部，可以await这个异步方法，也就可以在SafeFireAndForget内部拿到异步方法的异常进行根据是否发生异常去执行
    /// 两种不同的回调。
    /// </summary>
    /// <param name="task"></param>
    /// <param name="onCompleted"></param>
    /// <param name="onError"></param>
    async void SafeFireAndForgetAsync(Task task, Action? onCompleted = null, Action<Exception>? onError = null)
    {
        try
        {
            await task;
            onCompleted?.Invoke();
        }
        catch (Exception ex)
        {
            onError?.Invoke(ex);
        }
    }

    /// <summary>
    /// 方法二：使用ContinueWith()调用该方法，本方法内含有失败和成功的回调。
    /// </summary>
    /// <param name="task"></param>
    private void OnDataLoaded(Task task)
    {
        if (task.IsFaulted)
        {
            Console.WriteLine(task.Exception.InnerException); //失败的回调：这里抛出的是AggregateException，使用InnerException获取原本的异常。
        }
        IsDataLoaded = true; //成功的回调
    }

    /// <summary>
    /// 方法三：使用工厂函数创建实例，将构造函数写成私有，然后在工厂函数中调用构造函数并await异步方法，最后把实例返回给外部。
    /// </summary>
    /// <returns></returns>
    public static async Task<MyDataModel> CreateAsync()
    {
        var dataModel = new MyDataModel();
        try
        {
            await dataModel.LoadDataAsync();
            dataModel.IsDataLoaded = true;
            return dataModel;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

static class AsyncExtension
{
    public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource();
        var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token));
        if (completedTask != task)
        {
            cts.Cancel();
            throw new TimeoutException();
        }
        await task;
    }

    /// <summary>
    /// 方法一的extension方法。
    /// </summary>
    /// <param name="task"></param>
    /// <param name="onCompleted"></param>
    /// <param name="onError"></param>
    public static async void Await(this Task task, Action? onCompleted, Action<Exception>? onError)
    {
        try
        {
            await task;
            onCompleted?.Invoke();
        }
        catch (Exception ex)
        {
            onError?.Invoke(ex);
        }
    }
}

record Message(string ThreadName, string Msg);

class CanNotUseLockInAsyncTask
{
    //private readonly object _lock = new();
    //使用Nito.Asyncex提供的AsyncLock
    private readonly AsyncLock _lock = new();

    //initialCount:初始线程数
    //maxCount:最大线程数
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    /// <summary>
    /// 原异步方法
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public async Task<int> HeayJobAsync(int x)
    {
        await Task.Delay(100);
        return x * x;
    }

    /// <summary>
    /// 利用Nito.Asyncex在异步任务中使用锁实现同步机制。
    /// </summary>
    /// <returns></returns>
    public async Task<int> HeayJobLockAsync(int x)
    {
        //无法在一个lock语句中使用await：lock语句表达的意思是，其内部所有的操作，都必需要在一个单独的线程中完成，以保证对资源操作时不会有
        //其他线程的干扰；但await语句并不能保证该异步任务内部全部都在一个线程中进行(回忆line260的异步任务)，因此会导致lock语句中进行线程切换，
        //而这与lock的初衷相违背。另外await后续的代码默认情况下会切回到原线程中，这也同样会在lock语句中造成线程切换。
        //lock (_lock)
        //{
        //    await Task.Delay(100); //
        //    return x * x;
        //}

        //使用Nito.Asyncex提供的lock
        using (await _lock.LockAsync())
        {
            await Task.Delay(100);
            return x * x;
        }
    }

    /// <summary>
    /// 使用SemaphoreSlim在异步任务中实现同步机制。
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public async Task<int> HeayJobSemaphoreSlim(int x)
    {
        await _semaphore.WaitAsync();
        await Task.Delay(100);
        _semaphore.Release();
        return x * x;
    }
}

class ValueTaskDemo
{
    private ConcurrentDictionary<int, string> _cache = new();

    public ValueTaskDemo()
    {
        //如果不存在(1,"北京大学")就添加，否则将value改为"清华大学"
        _cache.AddOrUpdate(1, k => "北京大学", (k, v) => "北京大学");
        //_cache.AddOrUpdate(1, k => "北京大学", (k, v) => "清华大学");
    }

    public void ShowCache()
    {
        foreach (var (k, v) in _cache)
        {
            Console.WriteLine($"{k}: {v}");
        }
    }

    private async Task<string> GetFromDbAsync()
    {
        await Task.Delay(1000);
        return "清华大学";
    }

    /// <summary>
    /// 如果缓存命中，那么代码不会执行到await语句，那么这个异步任务，实际上返回的是同步内容；只有当缓存未命中，才会执行await代码，执行真正的异步任务。
    /// 这时如果使用Task<string>作为返回值，当缓存命中时，原本只需要返回string即可，但实际上需要包装成Task<string>，造成了额外的开销。
    /// </summary>
    /// <param name="k"></param>
    /// <returns></returns>
    public async Task<string> GetAsync(int k)
    {
        string v = string.Empty;
        if (_cache.TryGetValue(k, out v))
        {
            return v;
        }
        v = await GetFromDbAsync();
        _cache.TryAdd(k, v);
        return v;
    }

    /// <summary>
    /// 相比Task<string>，ValueTask<string>会判断返回值类型，只有在真正执行await后，才会返回Task，否则只返回string，从而降低了开销。
    /// </summary>
    /// <param name="k"></param>
    /// <returns></returns>
    public async ValueTask<string> GetValueTaskAsync(int k)
    {
        string v = string.Empty;
        if (_cache.TryGetValue(k, out v))
        {
            return v;
        }
        v = await GetFromDbAsync();
        _cache.TryAdd(k, v);
        return v;
    }
}

class CancelableThreadTask
{
    private Thread? _thread;
    private readonly Action _action; //待执行的任务
    private TaskCompletionSource? _tcs;
    private int _isRunning = 0; //0:未运行；1:正在运行
    private readonly Action<Exception>? _onError; //回调，用来在委托抛出异常时进行资源回收等操作。
    private readonly Action? _onCompletedSuccessfully; //回调，用来在委托被成功执行后调用。
    private readonly Action? _onCompleted; //回调，用来在委托被完成后调用，无论委托调用成功还是失败。

    public CancelableThreadTask(Action action, Action<Exception>? onError = null, Action? onCompletedSuccessfully = null,
                                Action? onCompleted = null)
    {
        ArgumentNullException.ThrowIfNull(action); //如果action为空则抛异常。
        _action = action;
        _onError = onError;
        _onCompletedSuccessfully = onCompletedSuccessfully;
        _onCompleted = onCompleted;
    }

    public Task RunAsync(CancellationToken token)
    {
        //使用_isRunning和0进行比较：
        //1._isRunning=0时，将_isRunning的值变为1，表示正在运行；CompareExchange的返回值是_isRunning的初始值0，不进入if语句；
        //2._isRunning=1时，_isRunning的值保持不变；CompareExchange的返回值是_isRunning的初始值1，进入if语句。
        //使用Interlocked.CompareExchange而不使用bool值去判断是否在运行的好处是：_isRunning可能会在多线程中被访问和修改，Interlocked的
        //原子性能保证其线程安全。
        if (Interlocked.CompareExchange(ref _isRunning, 1, 0) == 1)
            throw new InvalidOperationException("A task is already running!");
        _tcs = new TaskCompletionSource(); //给外部暴露_tcs，用来监测(反应)_thread内部任务的状态。
        //开辟新的线程去执行委托，并且将_tcs暴露给外部去反应线程内委托的执行状态。
        _thread = new Thread(() =>
        {
            try
            {
                _action(); //执行委托。
                _tcs.SetResult(); //将tcs设为RunToCompletion。
                _onCompletedSuccessfully?.Invoke();
            }
            catch (ThreadInterruptedException ex)
            {
                _tcs.TrySetCanceled(token); //当捕获了ThreadInterruptedException，说明外部触发了CancellationToken，token.Register中的回调被执行，
                                            //这时给_tcs也设置相应的Cancel状态。注意这里会抛一个TaskCanceledException。
                _onError?.Invoke(ex);
            }
            catch (Exception ex) //其他异常在此捕获，例如action内部的异常。
            {
                _tcs.SetException(ex);
                _onError?.Invoke(ex);
            }
            finally
            {
                Interlocked.Exchange(ref _isRunning, 0);
                _onCompleted?.Invoke(); //无论执行成功或失败，都去调用该委托。
            }
        });

        //注册当CancellationToken被触发时的回调：使用interrupt打断thread。
        token.Register(() =>
        {
            if (Interlocked.CompareExchange(ref _isRunning, 0, 1) == 1)
            {
                _thread.Interrupt();
                _thread.Join(); //在发出Interrupt信号后，还需要阻塞以确保该线程确实被杀死，否则有可能发生线程实际并未被杀死就触发了_tcs.TrySetCanceled(token)
            }

        });

        _thread.Start();
        return _tcs.Task; //返回tcs.task，以便外部监测内部委托的状态。
    }
}

class CancelableProcessTask
{
    private Process _process;
    private TaskCompletionSource? _tcs;
    private int _isRunning = 0;

    /// <summary>
    /// 用来新建进程以运行一个程序
    /// </summary>
    /// <param name="filename">程序路径</param>
    /// <param name="arguments">开启进程时的入参，
    /// 入参为cancelable时调用<seealso cref="ThirdPartyUtils.CancelableSyncMethod"> ThirdPartyUtils.CancelableSyncMethod()</seealso>；
    /// 入参为uncancelable时调用<seealso cref="ThirdPartyUtils.UncancelableSyncMethod"> ThirdPartyUtils.UncancelableSyncMethod()</seealso>
    /// 见<seealso cref="ThirdPartyMethodWrapper.Program.Main(string[])">ThirdPartyMethodWrapper</seealso>
    /// </param>
    public CancelableProcessTask(string filename, string arguments)
    {
        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            }
        };
    }

    public Task RunAsync(CancellationToken token)
    {
        if (Interlocked.CompareExchange(ref _isRunning, 1, 0) == 1)
            throw new InvalidOperationException("A task is already running!");

        _tcs = new TaskCompletionSource();

        token.Register(() =>
        {
            if (Interlocked.CompareExchange(ref _isRunning, 0, 1) == 1)
            {
                _process.Kill(); //使用process.kill()杀死进程
            }
        });

        _process.EnableRaisingEvents = true; //必须设置为true，process的终止才会触发Exited事件
        _process.Exited += (sender, e) => //给process的Exited事件添加一个回调，在回调里设置tcs的状态供外部追踪
        {
            if (_process.ExitCode == 0) //程序正常退出
                _tcs.SetResult();
            else
            {
                if (token.IsCancellationRequested) //如果token被cancel就设置task为canceled
                    _tcs.TrySetCanceled(token);
                else
                    _tcs.SetException(new Exception($"Process exited with code {_process.ExitCode}"));
            }
        };
        _process.Start();
        return _tcs.Task;
    }
}
#endregion
