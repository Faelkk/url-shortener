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

var port = builder.Configuration["APIPORT"] ?? "5010";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

DatabaseSeedeer.ApplyMigrationsAndSeed(app.Services);

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
