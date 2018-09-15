using System;
using System.Collections.Generic;
using Asdf.Domain.Actions;
using Asdf.Kernel;

namespace Asdf.Domain.Templates
{
    public class NodeTemplate : IHaveId<long?>
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string ActivatorType { get; set; }
        public string ActivatorAssembly { get; set; }

        public virtual IList<FieldTemplate> Fields { get; set; }

        public Type GetActivatorType() => Type.GetType($"{ActivatorType}, {ActivatorAssembly}");
        public Node Activate(params object[] args) => (Node)Activator.CreateInstance(GetActivatorType(), args);
    }
}
