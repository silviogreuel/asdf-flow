using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asdf.Domain.Users;

namespace Asdf.Domain.Actions
{
    public class DecisionNode : Node
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public OperationType Operation { get; set; }

        public DecisionNode() { }

        public DecisionNode(User user, string name, string left, OperationType operation, string right) : base(user, name)
        {
            this.Left = left;
            this.Operation = operation;
            this.Right = right;
        }

        public DecisionNode(User user, string name, string left, string operation, string right) : base(user, name)
        {
            this.Left = left;
            this.Operation = Enum.Parse<OperationType>(operation, true);
            this.Right = right;
        }


        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            var left = context[Left];
            var right = context[Right];
            bool result = false;

            switch (Operation)
            {
                case OperationType.Equal:
                    result = left == right;
                    break;
                case OperationType.NotEqual:
                    result = left != right;
                    break;
                case OperationType.GreaterThan:
                    result = left > right;
                    break;
                case OperationType.GreaterThanOrEqual:
                    result = left >= right;
                    break;
                case OperationType.LessThan:
                    result = left < right;
                    break;
                case OperationType.LessThanOrEqual:
                    result = left <= right;
                    break;
            }

            if (result)
                await NextPassAsync(context);
            else
                await NextFailAsync(context);
        }
    }
}
