using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplicationUi.Models;

namespace WebApplicationUi.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<IActionResult> Index()
     {
        var client =  _httpClientFactory.CreateClient();
         var responseMessage = await client.GetAsync("https://localhost:7255/Products");
        if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseJson =await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(responseJson);
            ViewBag.Message ="Success";
            return View(result);
        }
        else{
            ViewBag.Message = "Unsuccess";
        }
        return View(new List<ProductModel>());
     }
        public IActionResult Create()
     {
        return View();
     }
     [HttpPost]
     public async Task<IActionResult> Create(ProductModel model)
     {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var result = await client.PostAsync("https://localhost:7255/Products",content);
        if(result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View(model);
     }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var client =  _httpClientFactory.CreateClient();
         var responseMessage = await client.GetAsync($"https://localhost:7255/Products/{id}");
        if(responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var responseJson =await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProductModel>(responseJson);
            ViewBag.Message ="Success";
            return View(result);
        }
        else{
            ViewBag.Message = "Unsuccess";
        }
        return View(new List<ProductModel>());
       
    }
    [HttpPost]
    public async Task<IActionResult> Update(ProductModel model)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(model);
        var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var result = await client.PutAsync($"https://localhost:7255/Products",httpContent);
        if(result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Upload()
    {
        return View();
    }
    public async  Task<IActionResult> Upload(IFormFile formFile)
    {
        var client = _httpClientFactory.CreateClient();
        var stream = new MemoryStream();
        await formFile.CopyToAsync(stream);
        
        var bytes = stream.ToArray();
        
        ByteArrayContent content = new ByteArrayContent(bytes);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(formFile.ContentType);
        MultipartFormDataContent formData = new MultipartFormDataContent();
        formData.Add(content,"formFile",formFile.FileName);

        var result = await client.PostAsync("https://localhost:7255/Products/Upload",formData);
        if(result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View(formFile);
    }

}
