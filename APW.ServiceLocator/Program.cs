using APW.Architecture;
using APW.Models.Entities.Productdb;
using APW.ServiceLocator.Helper;
using APW.ServiceLocator.Services;
using APW.ServiceLocator.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRestProvider, RestProvider>();
builder.Services.AddScoped<IDogDataService, DogDataService>();
builder.Services.AddScoped<ITempDataService, TempDataService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IService<Product>, ProductService>();
//builder.Services.AddScoped<IService<Category>, CategoryService>();
builder.Services.AddScoped<IServiceMapper, ServiceMapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
