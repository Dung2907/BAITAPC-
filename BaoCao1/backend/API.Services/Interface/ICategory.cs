using TranAnhDung.API.Domain;
namespace TranAnhDung.API.Services.Interface 
{
    public interface ICategory
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(long CategoryId);
        Task<Response> Add(Category category);
        Task<Response> Update(Category category);
        Task<Response> Delete(long CategoryId);
    }
}
