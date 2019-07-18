using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversityRazorPage.Data;
using ContosoUniversityRazorPage.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversityRazorPage.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _schoolContext;

        public StudentRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _schoolContext.Students.ToListAsync();
        }

        public Task<Student> GetStudentById(int studentId)
        {
            return _schoolContext.Students.FindAsync(studentId);
        }

        public Task InsertStudent(Student student)
        {
            return _schoolContext.Students.AddAsync(student);
        }

        public async Task DeleteStudent(int studentId)
        {
            var student = await _schoolContext.Students.FindAsync(studentId);
            _schoolContext.Students.Remove(student);
        }

        public void UpdateStudent(Student student)
        {
            _schoolContext.Entry(student).State = EntityState.Modified;
        }

        public Task Save()
        {
            return _schoolContext.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _schoolContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}