using AutoMapper;
using StudentAdminPortal.API.DataModels.DTO;

namespace StudentAdminPortal.API.Profiles.AfterMap
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, DataModels.Domain.Student>
    {
        public void Process(UpdateStudentRequest source, DataModels.Domain.Student destination, ResolutionContext context)
        {
            destination.Address = new DataModels.Domain.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
