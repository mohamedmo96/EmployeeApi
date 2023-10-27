using Microsoft.EntityFrameworkCore;

namespace Employee.Model
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public Context()
        {

        }

        public DbSet<Employe> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employe>()
                .HasMany(e => e.Addresses)
                .WithOne(a => a.employe)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
