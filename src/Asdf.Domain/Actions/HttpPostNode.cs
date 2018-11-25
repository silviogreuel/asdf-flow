using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Asdf.Domain.Users;
using Serilog;

namespace Asdf.Domain.Actions
{
    public class HttpPostNode : Node
    {
        public string Url { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }

        public HttpPostNode() { }

        public HttpPostNode(User user, string name, string url, string content, string contentType) : base(user, name)
        {
            this.Url = url;
            this.Content = content;
            this.ContentType = contentType;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            var http = new HttpClient();
            var url = string.Format(new TemplateFormatProvider(), Url, context);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = string.Format(new TemplateFormatProvider(), Content, context);
            request.Content = new StringContent(content, Encoding.UTF8, ContentType);
            var response = await http.SendAsync(request);

            Log.Logger.Information($"POST {url} {ContentType} {content}");

            await NextPassAsync(context);
        }
    }
}
