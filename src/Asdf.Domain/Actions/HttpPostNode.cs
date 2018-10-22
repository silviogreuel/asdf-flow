using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Asdf.Domain.Users;

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
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Content = new StringContent(Content, Encoding.UTF8, ContentType);
            await http.SendAsync(request);
        }
    }
}
