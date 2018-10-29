using System;
using Asdf.Application.Processor.Templates;
using Asdf.Kernel.Utils;

namespace Asdf.Application.Processor.Dynamics {
    public class DynamicField {

      
        public DynamicField (string template) : this (new FieldTemplate (template)) { }

        public DynamicField (FieldTemplate template) {
            this.Template = template;
        }

        public FieldTemplate Template { get; private set; }
        public Type Type => Type.GetType (Template.Type.ToLower().GetDotNetType());
        public string Name => Template.Name;
        public object Value => Template.Type.ToLower ().GetDotNetValue(Template.Value);
    }
}