using Microsoft.EntityFrameworkCore;
using url_shortener.Application.Services;
using url_shortener.Infrastructure.Contexts;
using url_shortener.Infrastructure.DatabaseSeedeer;
using url_shortener.Infrastructure.Repository;
using url_shortener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddScoped<IDatabaseContext>(provider =>
    provider.GetRequiredService<DatabaseContext>()
);

builder.Services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();
builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "URL-Shortener API",
            Version = "v1",
            Description = "API desenvolvida para encurtamentos de URL",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Rafael Achtenberg",
                Email = "achtenberg.rafa@gmail.com",
                Url = new Uri("https://github.com/Faelkk"),
            },
        }
    );
});

var port = builder.Configuration["APIPORT"] ?? "5010";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

DatabaseSeedeer.ApplyMigrationsAndSeed(app.Services);

app.MapControllers();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API v1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

namespace url_shortener.Presentation
{
    public partial class Program { }
}
