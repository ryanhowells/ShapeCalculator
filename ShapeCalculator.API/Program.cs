using Microsoft.OpenApi.Models;
using ShapeCalculator.Core.Factories;
using ShapeCalculator.Core.Interfaces;
using ShapeCalculator.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShapeCalculator.API", Version = "v1" });
});

builder.Services.AddScoped<IShapeService, ShapeService>();
builder.Services.AddScoped<IShapeFactory, ShapeFactory>();


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
