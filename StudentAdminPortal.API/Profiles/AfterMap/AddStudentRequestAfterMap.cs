using AutoMapper;
using StudentAdminPortal.API.DataModels.DTO;

namespace StudentAdminPortal.API.Profiles.AfterMap
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModels.Domain.Student>
    {
        public void Process(AddStudentRequest source, DataModels.Domain.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Domain.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
