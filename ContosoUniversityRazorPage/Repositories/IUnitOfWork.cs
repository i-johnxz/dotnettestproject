using System.Threading.Tasks;
using ContosoUniversityRazorPage.Data;
using ContosoUniversityRazorPage.Models;

namespace ContosoUniversityRazorPage.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Student> Students { get; }

        IRepository<Course> Courses { get; }

        Task Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private SchoolContext _schoolContext;
        private BaseRepository<Student> _students;
        private BaseRepository<Course> _courses;

        public UnitOfWork(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }


        public IRepository<Student> Students => _students ?? (_students = new BaseRepository<Student>(_schoolContext));
        public IRepository<Course> Courses => _courses ?? (_courses = new BaseRepository<Course>(_schoolContext));

        public Task Commit()
        {
            return _schoolContext.SaveChangesAsync();
        }
    }
}