using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Narbona.MapperProfiles;
using PersonVM = Narbona.Models.PersonViewModel;
using PersonDto = Narbona.Database.Dto.Person;
using PersonModel = Narbona.Services.Models.Person;
using EmailDto = Narbona.Database.Dto.Email;
using EmailModel = Narbona.Services.Models.Email;



namespace Narbona.Tests.MapperProfiles
{
    [TestFixture]
    public class PersonProfileTests
    {
        private Mapper sut;

        [SetUp]
        public void Setup()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PersonProfile>();
            });

            sut = new Mapper(mapperConfiguration);
        }

        [Test]
        public void Map_PersonViewModel_To_PersonModel()
        {
            // Arrange
            var viewModel = new PersonVM
            {
                Name = "John",
                LastName = "Doe",
                Description = "Nothing to worry about",
                Emails = { "j.doe@google.com", "john@yahoo.com" }
            };

            // Act
            var result = sut.Map<PersonModel>(viewModel);

            // Assert
            Assert.That(result.Name, Is.EqualTo(viewModel.Name));
            Assert.That(result.LastName, Is.EqualTo(viewModel.LastName));
            Assert.That(result.Description, Is.EqualTo(viewModel.Description));
            CollectionAssert.AreEqual(result.Emails.Select(x => x.Value), viewModel.Emails);
        }

        [Test]
        public void Map_PersonModel_To_PersonViewModel()
        {
            // Arrange
            var model = new PersonModel
            {
                Name = "John",
                LastName = "Doe",
                Description = "Nothing to worry about",
                Emails =
                {
                    new EmailModel
                    {
                    Value = "j.doe@google.com"
                    },
                    new EmailModel
                    {
                    Value = "john@yahoo.com"
                    }
                }
            };

            // Act
            var result = sut.Map<PersonVM>(model);

            // Assert
            Assert.That(result.Name, Is.EqualTo(model.Name));
            Assert.That(result.LastName, Is.EqualTo(model.LastName));
            Assert.That(result.Description, Is.EqualTo(model.Description));
            CollectionAssert.AreEqual(result.Emails, model.Emails.Select(x => x.Value));
        }

        [Test]
        public void Map_PersonModel_To_PersonDto()
        {
            // Arrange
            var model = new PersonModel
            {
                Name = "John",
                LastName = "Doe",
                Description = "Nothing to worry about",
                Emails =
                {
                    new EmailModel
                    {
                    Value = "j.doe@google.com"
                    },
                    new EmailModel
                    {
                    Value = "john@yahoo.com"
                    }
                }
            };

            // Act
            var result = sut.Map<PersonDto>(model);

            // Assert
            Assert.That(result.Name, Is.EqualTo(model.Name));
            Assert.That(result.LastName, Is.EqualTo(model.LastName));
            Assert.That(result.Description, Is.EqualTo(model.Description));
            CollectionAssert.AreEqual(result.Emails.Select(x => x.Value), model.Emails.Select(x => x.Value));
        }
        
        [Test]
        public void Map_PersonDto_To_PersonModel()
        {
            // Arrange
            var dto = new PersonDto
            {
                Name = "John",
                LastName = "Doe",
                Description = "Nothing to worry about",
                Emails =
                {
                    new EmailDto
                    {
                    Value = "j.doe@google.com"
                    },
                    new EmailDto
                    {
                    Value = "john@yahoo.com"
                    }
                }
            };

            // Act
            var result = sut.Map<PersonDto>(dto);

            // Assert
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.LastName, Is.EqualTo(dto.LastName));
            Assert.That(result.Description, Is.EqualTo(dto.Description));
            CollectionAssert.AreEqual(result.Emails.Select(x => x.Value), dto.Emails.Select(x => x.Value));
        }
    }
}
