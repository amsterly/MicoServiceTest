using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicoServiceTest.Result
{
    public class PageResult : IHttpActionResult
    {
        object _value;
        HttpRequestMessage _request;

        public PageResult(object value, HttpRequestMessage request)
        {
            _value = value;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent(typeof(object), _value, new JsonMediaTypeFormatter()),
                RequestMessage = _request
            };

            Console.WriteLine(Thread.CurrentThread);
            return Task.FromResult(response);
        }
    }
}
