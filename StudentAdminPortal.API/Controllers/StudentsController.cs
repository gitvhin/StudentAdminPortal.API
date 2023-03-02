using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Repositories;
using System.Collections.Generic;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]

    public class StudentsController : Controller
    {
        private readonly IStudentRepository sqlStudentRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentsController(IStudentRepository sqlStudentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this.sqlStudentRepository = sqlStudentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var students = await sqlStudentRepository.GetStudentsAsync();

            // You can use this line of code below or you can use the 1 liner code in line 57 to directly map the Domain to DTO.
            #region Mapper
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
            #endregion

            var studentsDTO = mapper.Map<List<DataModels.DTO.Student>>(students);
            return Ok(studentsDTO);
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentByIdAsync")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] Guid studentId)
        {
            var student = await sqlStudentRepository.GetStudentByIdAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DataModels.DTO.Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] DataModels.DTO.UpdateStudentRequest request)
        {
            if (await sqlStudentRepository.Exists(studentId))
            {
                var updateStudent = await sqlStudentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Domain.Student>(request));
                if (updateStudent != null)
                {
                    return Ok(mapper.Map<DataModels.DTO.Student>(updateStudent));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsyc([FromRoute] Guid studentId)
        {
            if (await sqlStudentRepository.Exists(studentId))
            {
                var student = await sqlStudentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<DataModels.DTO.Student>(student));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] DataModels.DTO.AddStudentRequest request)
        {
            var student = await sqlStudentRepository.AddStudent(mapper.Map<DataModels.Domain.Student>(request));
            return CreatedAtAction(nameof(GetStudentByIdAsync), new { studentId = student.Id},
                mapper.Map<DataModels.DTO.Student>(student));

        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            if (await sqlStudentRepository.Exists(studentId))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName); 
                var fileImagePath = await imageRepository.Upload(profileImage, fileName);

                if (await sqlStudentRepository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
            }
            return NotFound();
        }
    }
}
