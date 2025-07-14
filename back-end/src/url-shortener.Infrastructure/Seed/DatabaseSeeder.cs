using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace url_shortener.Infrastructure.DatabaseSeedeer;

public class DatabaseSeedeer
{
    public static void ApplyMigrationsAndSeed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }
    }
}
