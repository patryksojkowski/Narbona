using AutoMapper;
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
        }
    }
}
