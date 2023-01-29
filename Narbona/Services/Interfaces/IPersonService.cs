using Narbona.Services.Models;

namespace Narbona.Services.Interfaces
{
    public interface IPersonService
    {
        void Add(Person person);
        IEnumerable<Person> ReadAll();
        void Update(Person person);
        void Delete(int id);
    }
}
