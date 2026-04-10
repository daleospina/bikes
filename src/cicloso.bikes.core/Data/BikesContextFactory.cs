using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cicloso.bikes.core.Data;

public class BikesContextFactory : IDesignTimeDbContextFactory<BikesContext>
{
    public BikesContext CreateDbContext(string[] args)
    {
        // Connection string dummy SOLO para poder crear el contexto
        var optionsBuilder = new DbContextOptionsBuilder<BikesContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=Dummy;User Id=sa;Password=Dummy123!;TrustServerCertificate=True");

        return new BikesContext(optionsBuilder.Options);
    }
}