﻿using StudentAdminPortal.API.DataModels.Domain;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();

        Task<Student> GetStudentByIdAsync(Guid studentId);
    }
}
