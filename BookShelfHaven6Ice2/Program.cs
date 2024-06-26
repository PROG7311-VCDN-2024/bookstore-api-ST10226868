using Microsoft.EntityFrameworkCore;
using BookShelfHaven6Ice2.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BookShelfHavenContext>(opt =>
    opt.UseInMemoryDatabase("BookShelfHaven"));
builder.Services.AddDbContext<BookShelfHavenContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("BookShelfHaven")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
