using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Value> Values { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Value>().HasData(
                new Value { Id = 1, Name = "value1" },
                new Value { Id = 2, Name = "value2" },
                new Value { Id = 3, Name = "value3" }
                );
            builder.Entity<Meeting>()
            .HasOne(m => m.Patient)
            .WithMany(p => p.Meetings);

        }
    }
}