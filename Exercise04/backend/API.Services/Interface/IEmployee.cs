using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
namespace API.Services.Interface
{
    public interface IEmployee
    {
        Task<Response> Add (Employee emp);
        Task<Response> Update (Employee emp);
        Task<Response> Delete(int employeeId);
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int employeeId); 
    }
}