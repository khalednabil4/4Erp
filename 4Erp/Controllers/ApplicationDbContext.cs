using _4Erp.Models;
using Microsoft.EntityFrameworkCore;

namespace _4Erp.Controllers
{
    public partial class masterContext : DbContext
    {
  

        public masterContext(DbContextOptions<masterContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CitiesAndRegions> Cities { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
    }
}
