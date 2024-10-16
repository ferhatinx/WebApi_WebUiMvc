using Microsoft.EntityFrameworkCore;

namespace WebApiApplication.Data;

public class ProductContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(new Category[]{new Category{Id=1,Name="Giyim"},new Category{Id=2,Name="Elektronik"}});
        modelBuilder.Entity<Product>().HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);
        base.OnModelCreating(modelBuilder);
    }
}
