using csharp_lazy_load_property.Models;
using Microsoft.EntityFrameworkCore;

namespace csharp_lazy_load_property.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}
