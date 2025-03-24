using dotnet_tech_test.Interfaces;
using dotnet_tech_test.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet-tech-test", Version = "v1" });
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IDataCalls, DataCalls>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet-tech-test v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/combined", async (IAlbumService albumService, ILogger<Program> logger) =>
    {
        logger.LogInformation("Calling GetCombinedDataAsync");
        var data = await albumService.GetCombinedDataAsync();
        return Results.Ok(data);
    })
    .WithName("GetCombinedData")
    .WithTags("CombinedData");

app.MapGet("/combined/{userId:int}", async (int userId, IAlbumService albumService, ILogger<Program> logger) =>
    {
        logger.LogInformation("Calling GetCombinedDataByUserIdAsync with userId: {UserId}", userId);
        var data = await albumService.GetCombinedDataByUserIdAsync(userId);
        return Results.Ok(data);
    })
    .WithName("GetCombinedDataByUserId")
    .WithTags("CombinedData");

app.Run();