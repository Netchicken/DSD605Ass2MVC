using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DSD605Ass2MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<DSD605Ass2MVC.Models.Customer>? Customer { get; set; }
        public DbSet<DSD605Ass2MVC.Models.Order>? Order { get; set; }
        public DbSet<DSD605Ass2MVC.Models.Staff>? Staff { get; set; }
        public DbSet<DSD605Ass2MVC.Models.Stock>? Stock { get; set; }
    }
}


