using System.Collections.Generic;

namespace Asdf.Kernel
{
    public interface IHaveContext
    {
        IDictionary<string, dynamic> Context { get; set; }
    }
}
