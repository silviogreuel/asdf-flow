using System.Threading.Tasks;

namespace Asdf.Kernel
{
    public interface ICanBeExecutableAsAction<in TParameter>
    {
        Task ExecuteAsync(TParameter parameter);
    }
}
