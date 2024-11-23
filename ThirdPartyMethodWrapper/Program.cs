using ThirdPartyClassLib;

namespace ThirdPartyMethodWrapper
{
    internal class Program
    {
        /// <summary>
        /// 用来调用第三方库里的方法，ThirdPartyMethodWrapper本身是一个console程序。
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            if (args[0] == "cancelable")
                ThirdPartyUtils.CancelableSyncMethod();
            else if (args[0] == "uncancelable")
                ThirdPartyUtils.UncancelableSyncMethod();
        }
    }
}
