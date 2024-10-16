using Microsoft.EntityFrameworkCore;
using WebApiApplication.Data;
using WebApiApplication.Interfaces;

namespace WebApiApplication.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _context;

    public ProductRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateProduct(Product product)
    {
       await _context.Products.AddAsync(product);
       await _context.SaveChangesAsync();
       return product;
    }

    public async Task DeleteProductAsync(int id)
    {
       var deletedEntity =await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
       _context.Products.Remove(deletedEntity);
       await _context.SaveChangesAsync();
    }


    public async Task<Product> GetByIdProductAsync(int id)
    {

        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<List<Product>> GetProductAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var unchanged =await _context.Products.SingleOrDefaultAsync(p => p.Id == product.Id);
        _context.Entry(unchanged).CurrentValues.SetValues(product);
         await _context.SaveChangesAsync();
    }

}
