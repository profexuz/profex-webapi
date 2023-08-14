using Profex.Persistance.Interfaces.Common;
using Profex.Service.Services.Categories.Layers;
using Profex.WebApi.Configurations.Layers;
using ProFex.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.ConfigureServiceLayer();
builder.ConfigureDataAccess();
//builder.Services.AddHttpContextAccessor();
    
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
app.UseStaticFiles();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();


app.Run();

