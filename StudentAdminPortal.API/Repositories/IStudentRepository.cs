﻿using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using StudentAdminPortal.API.DataModels.Domain;
using System.Diagnostics.Eventing.Reader;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(Guid studentId);
        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exists(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId, Student request);

        Task<Student> DeleteStudent(Guid studentId);
    }
}
