using AutoMapper;
using Domain= StudentAdminPortal.API.DataModels.Domain;
using  StudentAdminPortal.API.DataModels.DTO;
namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Domain.Student, Student>()
                .ReverseMap();
            CreateMap<Domain.Address, Address>()
                .ReverseMap();
            CreateMap<Domain.Gender, Gender>()
                .ReverseMap();
        }
    }
}
