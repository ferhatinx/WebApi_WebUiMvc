namespace WebApplicationUi.Models;

public class ProductModel
{
     public int Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public string? ImagePath { get; set; }
    
    public int? CategoryId { get; set; }
}
