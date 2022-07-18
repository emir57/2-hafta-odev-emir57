using _3_hafta.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace _3_hafta.DataAccess.Concrete.Contexts
{
    public class EfPatikaDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Folder> Folder { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Country> Country { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=123;Host=localhost;Port=5432;Database=PATIKA;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;");
        }
    }
}
