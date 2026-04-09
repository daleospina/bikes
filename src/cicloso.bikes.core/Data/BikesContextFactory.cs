using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cicloso.bikes.core.Data;

public class BikesContextFactory : IDesignTimeDbContextFactory<BikesContext>
{
    public BikesContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("BikesContext");

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("Environment variable 'BikesContext' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<BikesContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new BikesContext(optionsBuilder.Options);
    }
}