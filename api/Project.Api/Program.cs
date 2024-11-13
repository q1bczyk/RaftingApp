using api;
using Project.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
