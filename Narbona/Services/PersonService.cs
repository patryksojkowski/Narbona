using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narbona.Database;
using Narbona.Services.Interfaces;
using Narbona.Services.Models;

namespace Narbona.Services
{
    public class PersonService : IPersonService
    {
        private readonly PeopleContext peopleContext;
        private readonly IMapper mapper;

        public PersonService(PeopleContext peopleContext, IMapper mapper)
        {
            this.peopleContext = peopleContext;
            this.mapper = mapper;
        }

        public void Add(Person person)
        {
            var personDto = mapper.Map<Database.Dto.Person>(person);

            peopleContext.People.Add(personDto);
            peopleContext.SaveChanges();
        }

        public IEnumerable<Person> ReadAll()
        {
            var peopleDtos = peopleContext.People;

            var result = mapper.ProjectTo<Person>(peopleDtos);

            return result;
        }

        public void Update(Person person)
        {
            var currentPerson = peopleContext.People
                .Include(p => p.Emails)
                .FirstOrDefault(p => p.Id == person.Id);

            if (currentPerson == null)
            {
                throw new ArgumentOutOfRangeException(nameof(person.Id));
            }

            var updated = mapper.Map<Database.Dto.Person>(person);

            currentPerson.Name = updated.Name;
            currentPerson.LastName = updated.LastName;
            currentPerson.Description = updated.Description;
            currentPerson.Emails = updated.Emails;

            peopleContext.SaveChanges();
        }
    }
}
