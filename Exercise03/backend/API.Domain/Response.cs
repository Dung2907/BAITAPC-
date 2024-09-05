using System.Net;

namespace TranAnhDung.API.Domain
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public int EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}