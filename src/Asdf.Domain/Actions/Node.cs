using Asdf.Kernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asdf.Domain.Users;
using Serilog;

namespace Asdf.Domain.Actions
{
    public abstract class Node : IHaveId<long?>, ICanBeExecutableWithContext
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
        public virtual Node Pass { get; set; }
        public virtual Node Fail { get; set; }

        public Node() { }

        public Node(User user, string name)
        {
            this.Name = name;
            this.User = user;
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
                Log.Logger.Information($"EXCEPTION {e.Message}");
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
                Log.Logger.Information($"EXCEPTION {e.Message}");
                context["exception"] = e.Message;
            }
        }

        public abstract Task ExecuteAsync(IDictionary<string, dynamic> context);
    }
}
