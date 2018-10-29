using System;
using Asdf.Application.Processor.Templates;

namespace Asdf.Application.Processor.Dynamics {
    public class DynamicField {

        private static string GetDotNetType (string type) {
            switch (type) {
                case "guid":
                    return "System.Guid";
                case "bool":
                    return "System.Boolean";
                case "int":
                    return "System.Int32";
                case "float":
                    return "System.Single";
                case "double":
                    return "System.Double";
                case "string":
                default:
                    return "System.String";
            }
        }

        private static object GetDotNetValue (string type, string value) {
            switch (type) {
                case "guid":
                    return Guid.Parse(value);
                case "bool":
                    return Convert.ToBoolean(value);
                case "int":
                    return Convert.ToInt32(value);
                case "float":
                    return Convert.ToSingle(value);
                case "double":
                    return Convert.ToDouble(value);
                case "string":
                default:
                    return Convert.ToString (value);
            }
        }
        public DynamicField (string template) : this (new FieldTemplate (template)) { }

        public DynamicField (FieldTemplate template) {
            this.Template = template;
        }

        public FieldTemplate Template { get; private set; }
        public Type Type => Type.GetType (GetDotNetType (Template.Type.ToLower ()));
        public string Name => Template.Name;
        public object Value => GetDotNetValue (Template.Type.ToLower (), Template.Value);
    }
}