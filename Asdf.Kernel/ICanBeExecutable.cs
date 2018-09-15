using System.Threading.Tasks;

namespace Asdf.Kernel
{
    public interface ICanBeExecutable
    {
        Task ExecuteAsync();
    }
}
