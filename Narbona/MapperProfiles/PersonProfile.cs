using AutoMapper;
using PersonVM = Narbona.Models.PersonViewModel;
using PersonDto = Narbona.Database.Dto.Person;
using EmailDto = Narbona.Database.Dto.Email;
using PersonModel = Narbona.Services.Models.Person;
using EmailModel = Narbona.Services.Models.Email;

namespace Narbona.MapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonVM, PersonModel>().ForMember(pm => pm.Emails, opt => opt.MapFrom(src => ConvertToEmailModelList(src.Emails)));

            CreateMap<PersonModel, PersonVM>().ForMember(pm => pm.Emails, opt => opt.MapFrom(src => ConvertToEmailStringList(src.Emails)));

            CreateMap<PersonModel, PersonDto>();

            CreateMap<PersonDto, PersonModel>();

            CreateMap<EmailModel, EmailDto>();

            CreateMap<EmailDto, EmailModel>();
        }

        private IList<EmailModel> ConvertToEmailModelList(IList<string> emails) =>
          emails.Select(x => new EmailModel { Value = x }).ToList();

        private IList<string> ConvertToEmailStringList(IList<EmailModel> emails) =>
          emails.Select(x => x.Value).ToList();
    }
}
