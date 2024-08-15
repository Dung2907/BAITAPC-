using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
namespace API.Domain
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public int EmployeeId { get; set;}
        public int? DepartmentId { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}