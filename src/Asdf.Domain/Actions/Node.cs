using Asdf.Kernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asdf.Domain.Actions
{
    public abstract class Node : IHaveId<long?>, ICanBeExecutableWithContext
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public virtual Node Pass { get; set; }
        public virtual Node Fail { get; set; }

        public Node() { }

        public Node(string name)
        {
            this.Name = name;
        }

        public void AddPass(Node pass)
        {
            this.Pass = pass;
        }

        public void AddFail(Node fail)
        {
            this.Fail = fail;
        }

        public void Add(Node pass, Node fail)
        {
            AddPass(pass);
            AddFail(fail);
        }

        public async Task NextPassAsync(IDictionary<string, dynamic> context)
        {
            try
            {
                if (Pass == null) return;
                await Pass.ExecuteAsync(context);
            }
            catch (Exception e)
            {
                context["exception"] = e.Message;
            }
        }

        public async Task NextFailAsync(IDictionary<string, dynamic> context)
        {
            try
            {
                if (Fail == null) return;
                await Fail.ExecuteAsync(context);
            }
            catch (Exception e)
            {
                context["exception"] = e.Message;
            }
        }

        public abstract Task ExecuteAsync(IDictionary<string, dynamic> context);
    }
}
