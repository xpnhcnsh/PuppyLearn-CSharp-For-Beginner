namespace Ch1_L12_Supplymentary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void runBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //runBtn.Enabled = false;
                //以下演示四种Task的调用方法：

                //1.1.正确用法：默认情况下ConfigureAwait(true)
                await DoJobAsync();

                //1.2.ConfigureAwait(false)时：
                //使用SynchronizationContext.Current将UI线程所在的同步上下文传入方法，在方法内部操作progressBar对象。
                //await DoJobAsync(SynchronizationContext.Current!);

                //1.3.ConfigureAwait(false)时：本质和1.2相同。
                //使用IProgress<int>汇报进度，在方法外部操作progressBar对象。
                //var progress = new Progress<int>(x => progressBar.Value = x); //new progress时，可指定回调，此时的回调是在UI线程上执行的(progress内部
                //                                                              //存有new progress时的同步上下文，回调会在该同步上下文中被执行)。
                //                                                              //回调的参数x就是progress传递出来的参数i。
                //                                                              //DoJobAsync(progress)执行时，每当progress.report被触发，注册的回调就会在UI线程上执行，进度条就会被刷新。
                //await DoJobAsync(progress);

                //2.一发即忘，程序立刻会执行后续代码，button在DoJob期间变得可用了。
                //DoJobAsync(); 
                //DoJobAsync(SynchronizationContext.Current!);

                //3.Wait()会阻塞UI线程，导致：1.进度条不会被重新渲染；2.程序不会对鼠标事件进行响应；3.死锁
                //死锁的原因是：await Task.Delay(50)默认是ConfigureAwait(true)，表示该异步任务执行完毕后会回到之前的线程(此处为UI线程)继续后续代码，
                //但.Wait()将UI线程阻塞，因此即使await Task.Delay(50)已经执行完毕，UI线程也无法继续执行后续操作了。
                //DoJobAsync()里的await Task.Delay(50)说：我执行完了，请UI线程接管执行后续代码。
                //UI线程说：我阻塞了，我必须等待DoJob()执行完毕才能响应其他请求。
                //DoJobAsync()说：UI线程必须接着执行progressBar.Value = i我才能结束啊。
                //UI线程说：我阻塞了，DoJobAsync()必须执行完毕我才能响应其他请求啊。
                //...
                //如果在异步任务中配置ConfigureAwait(false)，表示后续代码不由UI线程执行，而是其他任意空闲线程执行，但这会引起另外的错误：
                //由于只有UI线程对progressBar对象有访问和控制权，因此其他任意线程中调用progressBar.Value = i都会报错。
                //DoJobAsync().Wait();
                //DoJobAsync(SynchronizationContext.Current!).Wait();

                //4.同Wait()。
                //DoJobAsync().GetAwaiter().GetResult();
                //DoJobAsync(SynchronizationContext.Current!).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                richTextBox.AppendText(ex.Message + "\n");
            }
            finally { runBtn.Enabled = true; }
        }

        /// <summary>
        /// ConfigureAwait(true)的版本，由于await之后会返回UI线程，因此progressBar.Value = i可以正常由UI线程执行。
        /// </summary>
        /// <returns></returns>
        async Task DoJobAsync()
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    await Task.Delay(50);  //默认情况下ConfigureAwait(true)，表示后续代码由UI线程继续执行。
                    progressBar.Value = i;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ConfigureAwait(false)的版本，需要借助SynchronizationContext将UI线程从外部传入。
        /// 更好的解决方法：<see cref="DoJob(IProgress{int})"/>
        /// </summary>
        /// <param name="synchronizationContext">传入UI线程所在的同步上下文</param>
        /// <returns></returns>
        async Task DoJobAsync(SynchronizationContext synchronizationContext)
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    await Task.Delay(50).ConfigureAwait(false); //ConfigureAwait(false)，会从线程池中取其他任意空闲线程执行后续代码，不会返回UI线程。
                                                                //此，如果需要操作progressBar，需要使用synchronizationContext，该参数由外部传入
                                                                //其表示UI线程所在的同步上下文，在这里通过Send()方法，通过UI线程执行progressBar.Value = i。
                    synchronizationContext.Send(_ => progressBar.Value = i, null);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ConfigureAwait(false)的推荐写法：不传入SynchronizationContext，而是传入IProgress进行进度汇报，将i作为参数汇报出去。
        /// 在new progress时，会将当前线程的同步上下文传入progress，而注册的回调会在该上下文中调用，本质上和传入SynchronizationContext是一样的。
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        async Task DoJobAsync(IProgress<int> progress)
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    await Task.Delay(50).ConfigureAwait(false);
                    progress.Report(i); //将i作为参数发送出去。
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
