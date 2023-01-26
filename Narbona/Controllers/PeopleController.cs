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
            return View();
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

        public IActionResult Read()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
