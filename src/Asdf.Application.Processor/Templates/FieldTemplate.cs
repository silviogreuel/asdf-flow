namespace Asdf.Application.Processor.Templates {
    public class FieldTemplate : Template {
        private const char SEPARATOR = ':';
        private const int COUNT = 3;
        public FieldTemplate (string line) : base (line, SEPARATOR, COUNT) { }

        public string Type => this[0];
        public string Name => this[1];
        public string Value => this[2];
    }
}