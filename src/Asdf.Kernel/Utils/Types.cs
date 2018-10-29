using System;

namespace Asdf.Kernel.Utils
{
    public static class Types
    {
        public static string GetDotNetType(this string type)
        {
            switch (type)
            {
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

        public static object GetDotNetValue(this string type, string value)
        {
            switch (type)
            {
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
                    return Convert.ToString(value);
            }
        }
    }
}
