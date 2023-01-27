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

        Mock<DbSet<PersonDto>> peopleSetMock;
        Mock<PeopleContext> peopleContextMock;
        Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            peopleSetMock = new Mock<DbSet<PersonDto>>();
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

        [Test]
        [Ignore("Sth is off about IQueryable mocking. https://learn.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking")]
        public void ReadAll_ShouldCallDbContextAndMapper_AndReturnCorrectResults()
        {
            // Arrange
            var people = GetPeopleQueryable();

            peopleSetMock.As<IQueryable<PersonDto>>().Setup(m => m.Provider).Returns(people.Provider);
            peopleSetMock.As<IQueryable<PersonDto>>().Setup(m => m.Expression).Returns(people.Expression);
            peopleSetMock.As<IQueryable<PersonDto>>().Setup(m => m.ElementType).Returns(people.ElementType);
            peopleSetMock.As<IQueryable<PersonDto>>().Setup(m => m.GetEnumerator()).Returns(people.GetEnumerator());

            // Act
            var results = sut.ReadAll();

            // Assert
            var john = results.ElementAt(0);
            Assert.That(john.Name, Is.EqualTo("John"));
            var jane = results.ElementAt(1);
            Assert.That(jane.Name, Is.EqualTo("Jane"));
        }

        private IQueryable<PersonDto> GetPeopleQueryable()
        {
            return new List<PersonDto>
            {
                new PersonDto
                {
                    Name = "John",
                    LastName = "Doe",
                    Description = "Nothing to worry about",
                    Emails =
                    {
                        new EmailDto
                        {
                            Value = "j.doe@google.com"
                        }
                    }
                },
                new PersonDto
                {
                    Name = "Jane",
                    LastName = "Doe",
                    Description = "It's 4:30 in the morning",
                    Emails =
                    {
                        new EmailDto
                        {
                            Value = "jane.doe@google.com"
                        }
                    }
                },
            }.AsQueryable();
        }
    }
}
