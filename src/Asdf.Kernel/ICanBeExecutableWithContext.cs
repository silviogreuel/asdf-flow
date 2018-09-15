using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asdf.Kernel
{
    public interface ICanBeExecutableWithContext
    {
        Task ExecuteAsync(IDictionary<string, dynamic> context);
    }
}
