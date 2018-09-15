using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asdf.Domain.Actions
{
    public class LogNode : Node
    {
        public string Level { get; set; }

        public LogNode() { }

        public LogNode(string level)
        {
            this.Level = level;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            context.ToList().ForEach((pair) => Console.WriteLine($"({Level}) {pair.Key}:{pair.Value}"));
            await Task.CompletedTask;
        }
    }
}
