using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Data;
using WebApiApplication.Interfaces;

namespace WebApiApplication.Controllers;

[EnableCors]
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    [HttpPost]
    public IActionResult Create(Product product)
    {
        var addedProduct = _productRepository.CreateProduct(product);
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var datas = await _productRepository.GetProductAsync();
        return Ok(datas);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var data = await _productRepository.GetByIdProductAsync(id);
        if (data != null)
        {
            return Ok(data);
        }
        return BadRequest("Ürün yok");
        
    }
    [HttpPut]
    public async Task<IActionResult> Update(Product product)
    {
        var entity = await _productRepository.GetByIdProductAsync(product.Id);
        if (entity != null)
        {

            await _productRepository.UpdateProductAsync(product);
            return Ok();
        }
        return NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         var entity = await _productRepository.GetByIdProductAsync(id);
        if (entity != null)
        {

            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
        return NotFound();
    }
    [HttpPost("Upload")]
    public async Task<IActionResult> Upload([FromForm]IFormFile formFile)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",formFile.FileName);
        var stream = new FileStream(path, FileMode.Create); 
        await formFile.CopyToAsync(stream);
        return Ok();
    }
}
