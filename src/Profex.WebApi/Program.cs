using Profex.WebApi.Configurations;
using Profex.WebApi.Configurations.Layers;
using Profex.WebApi.Middlewares;
using ProFex.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.ConfigureJwtAuth();
builder.ConfigureSwaggerAuth();
builder.ConfigureCORSPolicy();
builder.ConfigureServiceLayer();
builder.ConfigureDataAccess();


var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseMiddleware<CrosOriginAccessMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

