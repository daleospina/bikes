using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cicloso.bikes.core.Data;

public class BikesContextFactory : IDesignTimeDbContextFactory<BikesContext>
{
    public BikesContext CreateDbContext(string[] args)
    {
        var basePath = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "cicloso.bikes.api")
        );

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BikesContext>();
        var connectionString = configuration.GetConnectionString("BikesContext");

        optionsBuilder.UseSqlServer(connectionString);

        return new BikesContext(optionsBuilder.Options);
    }
}