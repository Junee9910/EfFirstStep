using System.Collections.Generic;

namespace EfFirstStep
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int? Credits { get; set; }
        public int DepartmentID { get; set; }
        public Dapartment Dapartment { get; set; }
        public List<CourseAssignment> CourseAssignments { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
