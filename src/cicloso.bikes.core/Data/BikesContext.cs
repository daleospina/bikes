using Microsoft.EntityFrameworkCore;
using cicloso.bikes.core.Models;

namespace cicloso.bikes.core.Data
{
    public class BikesContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; } = default!;

        public BikesContext(DbContextOptions<BikesContext> options)
            : base(options)
        {
        }
    }
}