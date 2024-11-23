namespace ThirdPartyClassLib
{
    public class ThirdPartyUtils
    {
        public static bool CancelableSyncMethod()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100); //这里阻塞了线程
            }
            Console.WriteLine("Third party cancelable job done!");
            return true;
        }

        public static bool UncancelableSyncMethod() 
        {
            long sum = 0;
            for (long i = 0; i < 10_000_000_000; i++) 
            {
                sum++; //在这个方法里并未被阻塞
            }
            Console.WriteLine("Third party uncancelable job done!");
            return true;
        }
    }
}
