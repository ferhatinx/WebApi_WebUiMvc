using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Data;

namespace WebApiApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ProductContext _context;

    public CategoriesController(ProductContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
       var datas =await _context.Categories.Include(x=>x.Products).ToListAsync();
    
        return Ok(datas);
    }
}
