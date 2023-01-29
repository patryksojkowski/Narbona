using Microsoft.EntityFrameworkCore;
using Narbona.Database.Dto;

namespace Narbona.Database
{
    public class PeopleContext : DbContext
    {
        public PeopleContext()
        {
        }

        public PeopleContext(DbContextOptions<PeopleContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasOne(e => e.Person)
                .WithMany(p => p.Emails)
                .IsRequired();
        }

        public virtual DbSet<Person> People { get; set; }
    }
}
