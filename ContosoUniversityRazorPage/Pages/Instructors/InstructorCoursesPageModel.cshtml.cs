using System.Collections.Generic;
using System.Linq;
using ContosoUniversityRazorPage.Data;
using ContosoUniversityRazorPage.Models;
using ContosoUniversityRazorPage.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversityRazorPage.Pages.Instructors
{
    public class InstructorCoursesPageModel : PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(SchoolContext context,
                                               Instructor instructor)
        {
            var allCourses = context.Courses;
            var instructorCourses = new HashSet<int>(
                instructor.CourseAssignments.Select(c => c.CourseID)
            );
            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
        }

        public void UpdateInstructorCourses(SchoolContext context,
            string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses =
                new HashSet<int>(instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseID));

            foreach (var course in context.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.CourseAssignments.Add(new CourseAssignment
                        {
                            InstructorID = instructorToUpdate.ID,
                            CourseID = course.CourseID
                        });
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        CourseAssignment courseToRemove =
                            instructorToUpdate.CourseAssignments.SingleOrDefault(i => i.CourseID == course.CourseID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}