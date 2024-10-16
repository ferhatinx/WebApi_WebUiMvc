using WebApiApplication.Data;

namespace WebApiApplication.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductAsync();
     Task<Product> GetByIdProductAsync(int id);
     Task<Product> CreateProduct(Product product);
     Task UpdateProductAsync(Product product);
     Task DeleteProductAsync(int id);

}
