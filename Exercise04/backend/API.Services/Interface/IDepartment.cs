using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
namespace API.Services.Interface
{
    public interface IDepartment
    {
        Task<List<Department>> GetAll();
        Task<Department> GetById(int departmentId);
        Task<Response> Add(Department department);
        Task<Response> Delete(int departmentId); // Add this line
    }
}