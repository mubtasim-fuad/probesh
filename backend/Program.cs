using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Probesh.Api.Data;
using Probesh.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<ProbeshDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProbeshDatabase")));
builder.Services.AddScoped<AccessibilityScoreCalculator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDevelopment", policy =>
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AngularDevelopment");
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    service = "Probesh API",
    utc = DateTime.UtcNow
}));

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProbeshDbContext>();
    await db.Database.EnsureCreatedAsync();
    await SeedData.InitializeAsync(db);
}

await app.RunAsync();

public partial class Program
{
}
