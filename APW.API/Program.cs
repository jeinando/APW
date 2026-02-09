using APW.Data.MSSQL;



 using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = "Server=localhost,1433;Database=ProductDB;User Id=sa;Password=*APW1234;TrustServerCertificate=True;";

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); 

app.UseAuthorization();
app.MapControllers();

app.Run();