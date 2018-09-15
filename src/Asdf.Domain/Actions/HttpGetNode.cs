using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Asdf.Domain.Actions
{
    public class HttpGetNode : Node
    {
        public string Url { get; set; }
        public string Field { get; set; }

        public HttpGetNode() { }

        public HttpGetNode(string name, string url, string field) : base(name)
        {
            this.Url = url;
            this.Field = field;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            var url = string.Format(new TemplateFormatProvider(), Url, context);
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await http.SendAsync(request);
            context[Field] = await response.Content.ReadAsStringAsync();
            await NextPassAsync(context);
        }
    }
}
