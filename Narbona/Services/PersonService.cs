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
            var personDto = peopleContext.People
                .Include(p => p.Emails)
                .FirstOrDefault(p => p.Id == person.Id);

            if (personDto == null)
            {
                throw new ArgumentOutOfRangeException(nameof(person.Id));
            }

            var updated = mapper.Map<Database.Dto.Person>(person);

            personDto.Name = updated.Name;
            personDto.LastName = updated.LastName;
            personDto.Description = updated.Description;
            personDto.Emails = updated.Emails;

            peopleContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var personDto = peopleContext.People.FirstOrDefault(p => p.Id == id);

            if (personDto == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            peopleContext.People.Remove(personDto);
            peopleContext.SaveChanges();
        }
    }
}
