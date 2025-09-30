using Microsoft.EntityFrameworkCore;
using MangaReaderAPI.Data;
using MangaReaderAPI.Repositories;
using MangaReaderAPI.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISeriesRepository, SeriesRepository>();
builder.Services.AddScoped<ISeriesService, SeriesService>();

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
app.MapControllers();
app.Run();
