using TranAnhDung.API.Domain;
namespace TranAnhDung.API.Services.Interface
{
    public interface IProduct
    {
        Task<Response> Add(Product pro);
        Task<Response> Update(Product pro);
        Task<Response> Delete(long ProductId);
        Task<List<Product>> GetAll();
        Task<Product> GetById(long ProductId);
    }
}
