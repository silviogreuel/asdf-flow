using System.Threading.Tasks;

namespace Asdf.Kernel.Utils
{
    public class GlobalBus
    {
        public delegate Task PublishDelegate(string exchange, string routing, byte[] body);
        public static PublishDelegate Publish;
    }
}
