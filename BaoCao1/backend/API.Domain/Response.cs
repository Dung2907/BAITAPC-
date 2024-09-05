using System.Net;
namespace TranAnhDung.API.Domain
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public int ProductId { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}