using Microsoft.EntityFrameworkCore;
using WebApiPerson.Models;
using static WebApiPerson.Models.EntitiesModels;

namespace WebApiPerson.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }


        public DbSet<Sale> Sales { get; set; }


        public DbSet<SaleDetail> SaleDetails { get; set; }


        public DbSet<Usuario> Usuarios { get; set; }
    }
}
