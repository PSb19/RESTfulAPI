using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Controllers;
using RESTfulAPI.Interfaces;
using RESTfulAPI.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("dbName"));
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskResource, TaskResource>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
