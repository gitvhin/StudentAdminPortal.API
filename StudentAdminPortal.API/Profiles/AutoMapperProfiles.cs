using AutoMapper;
using Domain = StudentAdminPortal.API.DataModels.Domain;
using StudentAdminPortal.API.DataModels.DTO;
using StudentAdminPortal.API.Profiles.AfterMap;


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
            CreateMap<UpdateStudentRequest, Domain.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();

        }
    }
}
