using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiApplication.Data;
using WebApiApplication.Interfaces;
using WebApiApplication.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>(opt =>{
    opt.UseSqlite(builder.Configuration.GetConnectionString("local"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddControllers().AddNewtonsoftJson(opt=>{
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseRouting();
app.UseCors(opt =>{
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});

app.UseAuthorization();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();
