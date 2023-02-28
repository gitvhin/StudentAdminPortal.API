using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Repositories;
using System.Collections.Generic;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository sqlStudentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository sqlStudentRepository, IMapper mapper)
        {
            this.sqlStudentRepository = sqlStudentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var students = await sqlStudentRepository.GetStudentsAsync();

            // You can use this page line of code below or you can you the 1 liner code in line 57 to directly map the Domain to DTO.

            //var studentsDTO = new List<DataModels.DTO.Student>();
            //students.ForEach(student =>
            //{
            //    var studentDTO = new DataModels.DTO.Student()
            //    {
            //        Id = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName,
            //        DateOfBirth = student.DateOfBirth,
            //        Email = student.Email,
            //        Mobile = student.Mobile,
            //        ProfileImageUrl = student.ProfileImageUrl,
            //        GenderId = student.GenderId,
            //        Address = new DataModels.DTO.Address()
            //        {
            //            Id = student.Address.Id,
            //            PhysicalAddress = student.Address.PhysicalAddress,
            //            PostalAddress = student.Address.PostalAddress
            //        },
            //        Gender = new DataModels.DTO.Gender()
            //        { 
            //            Id = student.Gender.Id,
            //            Description = student.Gender.Description
            //        }
            //    };
            //    studentsDTO.Add(studentDTO);
            //});


            var studentsDTO = mapper.Map<List<DataModels.DTO.Student>>(students);
            return Ok(studentsDTO);
        }
    }
}
