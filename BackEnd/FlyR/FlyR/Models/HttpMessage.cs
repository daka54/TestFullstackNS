using System.Net;

namespace FlyR.Models
{
    public class HttpMessage<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public T Data { get; set; }
    }
}
