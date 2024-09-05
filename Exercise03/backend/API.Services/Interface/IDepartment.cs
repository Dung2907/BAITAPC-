using TranAnhDung.API.Domain;
namespace TranAnhDung.API.Services.Interface
{
    public interface IDepartment
    {
        Task<List<Department>> GetAll();
        Task<Department> GetById(int departmentId);
        Task<Response> Add(Department department);
        Task<Response> Delete(int departmentId); // Add this line
    }
}