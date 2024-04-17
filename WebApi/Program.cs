using Microsoft.Extensions.FileProviders;
using Core;
using Infrastructure;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("ConnectionSqlite") ?? throw new InvalidOperationException("Connection string 'ConnectionSqlite' not found.");

builder.Services.AddDBContext(connection);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddRepository();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddValidator();

builder.Services.AddAutoMapper();

builder.Services.AddCustomService();

builder.Services.AddIdentity();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
if (!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/images"
});

app.UseAuthorization();

app.MapControllers();

app.SeedData();

app.Run();
