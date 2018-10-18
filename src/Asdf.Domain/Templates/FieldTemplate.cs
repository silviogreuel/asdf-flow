using Asdf.Kernel;

namespace Asdf.Domain.Templates
{
    public class FieldTemplate : IHaveId<long?>
    {
        public long? Id { get; set; }
        public long? NodeTemplateId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
