using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

        optionsBuilder.UseSqlServer(
            "Server=url-shortener-db;Database=UrlShortenerDb;User Id=sa;Password=YourStrong!Passw0rd;"
        );

        return new DatabaseContext(optionsBuilder.Options);
    }
}
