using System.Net;
using YES.Dtos.API;

namespace YES.Dtos
{
    public class APIResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }
        public bool Success { get; set; }
        public APIException Error { get; set; }
    }
    public class APIResponse
    {
        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }
        public bool Success { get; set; }
        public APIException Error { get; set; }
    }
}
