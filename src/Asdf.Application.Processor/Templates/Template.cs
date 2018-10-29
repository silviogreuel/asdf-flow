using System;

namespace Asdf.Application.Processor.Templates {
    public class Template {

        private static TType Guard<TException, TType> (Func<TType> func) where TException : Exception {
            try {
                return func ();
            } catch (TException) {
                return default (TType);
            }
        }
        public Template (string line, char separator, int count) {
            this.Line = line;
            this.Separator = separator;
            this.Count = count;
        }
        public string Line { get; private set; }
        public char Separator { get; private set; }

        public string this [int index] => Guard<IndexOutOfRangeException, string> (() => Line.Split (new char[] { Separator }, Count) [index].Trim ());

        public int Count { get; private set; }
    }
}