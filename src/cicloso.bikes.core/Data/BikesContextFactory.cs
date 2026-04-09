using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cicloso.bikes.core.Data;

public class BikesContextFactory : IDesignTimeDbContextFactory<BikesContext>
{
    public BikesContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        // EF se ejecuta desde la raíz del repo en CI
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("src/cicloso.bikes.api/appsettings.json", optional: false)
            .AddJsonFile("src/cicloso.bikes.api/appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BikesContext>();

        var connectionString = configuration.GetConnectionString("BikesContext");

        optionsBuilder.UseSqlServer(connectionString);

        return new BikesContext(optionsBuilder.Options);
    }
}