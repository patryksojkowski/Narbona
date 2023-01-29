using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Narbona.Models;
using Narbona.Services.Interfaces;

using PersonModel = Narbona.Services.Models.Person;

namespace Narbona.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPersonService personService;

        public PeopleController(IMapper mapper, IPersonService personService)
        {
            this.mapper = mapper;
            this.personService = personService;
        }

        public IActionResult Index()
        {
            IEnumerable<PersonViewModel> people;

            try
            {
                people = Read();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return View(people);
        }

        public IActionResult Create(PersonViewModel personViewModel)
        {
            try
            {
                var personModel = mapper.Map<PersonModel>(personViewModel);

                personService.Add(personModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        public IEnumerable<PersonViewModel> Read()
        {
            IEnumerable<PersonViewModel> result;
            try
            {
                var people = personService.ReadAll();

                result = mapper.Map<IEnumerable<PersonViewModel>>(people);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public IActionResult Update(PersonViewModel personViewModel)
        {
            try
            {
                var personModel = mapper.Map<PersonModel>(personViewModel);

                personService.Update(personModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        public IActionResult Delete(int personId)
        {
            try
            {
                personService.Delete(personId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
