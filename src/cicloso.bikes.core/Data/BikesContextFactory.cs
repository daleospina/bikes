using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cicloso.bikes.core.Data;

public class BikesContextFactory : IDesignTimeDbContextFactory<BikesContext>
{
    public BikesContext CreateDbContext(string[] args)
    {
        var root = Directory.GetCurrentDirectory();

        var appSettingsPath = Directory
            .GetFiles(root, "appsettings.json", SearchOption.AllDirectories)
            .First(path => path.Contains("cicloso.bikes.api"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(appSettingsPath)!)
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BikesContext>();
        optionsBuilder.UseSqlServer(
            configuration.GetConnectionString("BikesContext")
        );

        return new BikesContext(optionsBuilder.Options);
    }
}