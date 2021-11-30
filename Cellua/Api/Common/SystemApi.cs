using System.Threading;

namespace Cellua.Api.Common
{
    public class SystemApi
    {
        public static void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }
    }
}