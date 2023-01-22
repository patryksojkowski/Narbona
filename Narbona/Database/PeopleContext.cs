using Microsoft.EntityFrameworkCore;
using Narbona.Database.Dto;

namespace Narbona.Database
{
  public class PeopleContext : DbContext
  {
    public PeopleContext(DbContextOptions<PeopleContext> options)
      :base(options)
    {
    }

    public DbSet<Person> People { get; set; }
  }
}
