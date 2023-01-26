using AutoMapper;
using Moq;
using Narbona.Database;
using Narbona.Services;

using PersonDto = Narbona.Database.Dto.Person;
using PersonModel = Narbona.Services.Models.Person;
using EmailDto = Narbona.Database.Dto.Email;
using EmailModel = Narbona.Services.Models.Email;
using Microsoft.EntityFrameworkCore;

namespace Narbona.Tests.Services
{
    [TestFixture]
    internal class PersonServiceTests
    {
        private PersonService sut;

        Mock<PeopleContext> peopleContextMock;
        Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            var peopleSetMock = new Mock<DbSet<PersonDto>>();
            peopleContextMock = new Mock<PeopleContext>();
            peopleContextMock.Setup(x => x.People).Returns(peopleSetMock.Object);

            mapperMock = new Mock<IMapper>();

            sut = new PersonService(peopleContextMock.Object, mapperMock.Object);
        }

        [Test]
        public void Add_ShouldCallDbContext_and_Mapper()
        {
            // Arrange
            var personModel = new PersonModel
            {
                Name = "Test",
                LastName = "LastName",
                Description = "Description",
                Emails = new List<EmailModel>
                {
                  new EmailModel
                  {
                    Value = "test@lastName.com"
                  }
                }
            };

            var personDto = new PersonDto
            {
                Name = "Test",
                LastName = "LastName",
                Description = "Description",
                Emails = new List<EmailDto>
                {
                  new EmailDto
                  {
                    Value = "test@lastName.com"
                  }
                }
            };

            peopleContextMock.Setup(x => x.People.Add(personDto));
            mapperMock.Setup(x => x.Map<PersonDto>(personModel)).Returns(personDto);

            // Act
            sut.Add(personModel);

            // Assert
            mapperMock.Verify(x => x.Map<PersonDto>(personModel));
            peopleContextMock.Verify(x => x.People.Add(It.IsAny<PersonDto>()));
        }
    }
}
