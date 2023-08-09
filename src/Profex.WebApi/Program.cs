using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Categories;
using Profex.DataAccsess.Repositories.Categories;
using Profex.Persistance.Interfaces.Categories;
using Profex.Persistance.Interfaces.Common;
using Profex.Service.Services.Categories;
using Profex.Service.Services.Categories.Layers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IPaginator, Paginator>();
builder.Services.AddScoped <IPaginator, Paginator>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped<IPaginator, Paginator>();




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

