using Asdf.Kernel;

namespace Asdf.Domain.Devices
{
    public class Device : IHaveId<long?>, IHaveName
    {
        public long? Id { get; set; }
        public string Name { get; set; }

        public Device() { }

        public Device(string name)
        {
            this.Name = name;
        }
    }
}
