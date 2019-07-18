using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversityRazorPage.Models;

namespace ContosoUniversityRazorPage.Repositories
{
    public interface IStudentRepository : IDisposable
    {

        Task<IEnumerable<Student>> GetStudents();

        Task<Student> GetStudentById(int studentId);

        Task InsertStudent(Student student);

        Task DeleteStudent(int studentId);

        void UpdateStudent(Student student);

        Task Save();
    }
}