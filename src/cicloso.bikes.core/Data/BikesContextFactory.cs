using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cicloso.bikes.core.Data;

public class BikesContextFactory : IDesignTimeDbContextFactory<BikesContext>
{
    public BikesContext CreateDbContext(string[] args)
    {
        string? connectionString = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--connection" && i + 1 < args.Length)
            {
                connectionString = args[i + 1];
                break;
            }
        }

        connectionString ??= Environment.GetEnvironmentVariable("BikesContext");

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("No connection string provided.");

        var optionsBuilder = new DbContextOptionsBuilder<BikesContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new BikesContext(optionsBuilder.Options);
    }
}