using System.Threading.Tasks;

namespace Asdf.Kernel
{
    public interface ICanBeExecutableAsFunction<TParameter, TReturn>
    {
        Task<TReturn> ExecuteAsync(TParameter parameter);
    }
}
