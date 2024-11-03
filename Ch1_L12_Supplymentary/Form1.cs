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
                //������ʾ����Task�ĵ��÷�����

                //1.1.��ȷ�÷���Ĭ�������ConfigureAwait(true)
                await DoJobAsync();

                //1.2.ConfigureAwait(false)ʱ��
                //ʹ��SynchronizationContext.Current��UI�߳����ڵ�ͬ�������Ĵ��뷽�����ڷ����ڲ�����progressBar����
                //await DoJobAsync(SynchronizationContext.Current!);

                //1.3.ConfigureAwait(false)ʱ�����ʺ�1.2��ͬ��
                //ʹ��IProgress<int>�㱨���ȣ��ڷ����ⲿ����progressBar����
                //var progress = new Progress<int>(x => progressBar.Value = x); //new progressʱ����ָ���ص�����ʱ�Ļص�����UI�߳���ִ�е�(progress�ڲ�
                //                                                              //����new progressʱ��ͬ�������ģ��ص����ڸ�ͬ���������б�ִ��)��
                //                                                              //�ص��Ĳ���x����progress���ݳ����Ĳ���i��
                //                                                              //DoJobAsync(progress)ִ��ʱ��ÿ��progress.report��������ע��Ļص��ͻ���UI�߳���ִ�У��������ͻᱻˢ�¡�
                //await DoJobAsync(progress);

                //2.һ���������������̻�ִ�к������룬button��DoJob�ڼ��ÿ����ˡ�
                //DoJobAsync(); 
                //DoJobAsync(SynchronizationContext.Current!);

                //3.Wait()������UI�̣߳����£�1.���������ᱻ������Ⱦ��2.���򲻻������¼�������Ӧ��3.����
                //������ԭ���ǣ�await Task.Delay(50)Ĭ����ConfigureAwait(true)����ʾ���첽����ִ����Ϻ��ص�֮ǰ���߳�(�˴�ΪUI�߳�)�����������룬
                //��.Wait()��UI�߳���������˼�ʹawait Task.Delay(50)�Ѿ�ִ����ϣ�UI�߳�Ҳ�޷�����ִ�к��������ˡ�
                //DoJobAsync()���await Task.Delay(50)˵����ִ�����ˣ���UI�߳̽ӹ�ִ�к������롣
                //UI�߳�˵���������ˣ��ұ���ȴ�DoJob()ִ����ϲ�����Ӧ��������
                //DoJobAsync()˵��UI�̱߳������ִ��progressBar.Value = i�Ҳ��ܽ�������
                //UI�߳�˵���������ˣ�DoJobAsync()����ִ������Ҳ�����Ӧ�������󰡡�
                //...
                //������첽����������ConfigureAwait(false)����ʾ�������벻��UI�߳�ִ�У�����������������߳�ִ�У��������������Ĵ���
                //����ֻ��UI�̶߳�progressBar�����з��ʺͿ���Ȩ��������������߳��е���progressBar.Value = i���ᱨ��
                //DoJobAsync().Wait();
                //DoJobAsync(SynchronizationContext.Current!).Wait();

                //4.ͬWait()��
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
        /// ConfigureAwait(true)�İ汾������await֮��᷵��UI�̣߳����progressBar.Value = i����������UI�߳�ִ�С�
        /// </summary>
        /// <returns></returns>
        async Task DoJobAsync()
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    await Task.Delay(50);  //Ĭ�������ConfigureAwait(true)����ʾ����������UI�̼߳���ִ�С�
                    progressBar.Value = i;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ConfigureAwait(false)�İ汾����Ҫ����SynchronizationContext��UI�̴߳��ⲿ���롣
        /// ���õĽ��������<see cref="DoJob(IProgress{int})"/>
        /// </summary>
        /// <param name="synchronizationContext">����UI�߳����ڵ�ͬ��������</param>
        /// <returns></returns>
        async Task DoJobAsync(SynchronizationContext synchronizationContext)
        {
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    await Task.Delay(50).ConfigureAwait(false); //ConfigureAwait(false)������̳߳���ȡ������������߳�ִ�к������룬���᷵��UI�̡߳�
                                                                //�ˣ������Ҫ����progressBar����Ҫʹ��synchronizationContext���ò������ⲿ����
                                                                //���ʾUI�߳����ڵ�ͬ�������ģ�������ͨ��Send()������ͨ��UI�߳�ִ��progressBar.Value = i��
                    synchronizationContext.Send(_ => progressBar.Value = i, null);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ConfigureAwait(false)���Ƽ�д����������SynchronizationContext�����Ǵ���IProgress���н��Ȼ㱨����i��Ϊ�����㱨��ȥ��
        /// ��new progressʱ���Ὣ��ǰ�̵߳�ͬ�������Ĵ���progress����ע��Ļص����ڸ��������е��ã������Ϻʹ���SynchronizationContext��һ���ġ�
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
                    progress.Report(i); //��i��Ϊ�������ͳ�ȥ��
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
