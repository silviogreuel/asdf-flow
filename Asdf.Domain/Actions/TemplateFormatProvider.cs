using System;
using System.Collections.Generic;

namespace Asdf.Domain.Actions
{
    //Extract common interface
    public class TemplateFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var context = arg as Dictionary<string, dynamic>;

            if (context == null)
                return string.Empty;

            if (!context.ContainsKey(format))
                return string.Empty;

            return context[format];
        }

        public object GetFormat(Type formatType)
        {
            return this;
        }
    }
}
