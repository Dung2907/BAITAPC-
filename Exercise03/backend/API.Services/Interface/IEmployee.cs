
using TranAnhDung.API.Domain;

using Microsoft.AspNetCore.Http;
namespace TranAnhDung.API.Services.Interface
{
    public interface IEmployee
    {
        Task<Response> Add(Employee empDto, IFormFile file);
        Task<Response> Update(Employee emp);
        Task<Response> Delete(int employeeId);
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int employeeId);
    }
}