using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Narbona.Database
{
  public class PeopleContextFactory : IDesignTimeDbContextFactory<PeopleContext>
  {
    public PeopleContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<PeopleContext>();
      optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NarbonaDatabase;Trusted_Connection=true");

      return new PeopleContext(optionsBuilder.Options);
    }
  }
}
