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

        public virtual DbSet<Person> People { get; set; }
    }
}
