using System;
using System.Collections.Generic;

namespace EfFirstStep
{
    public class Dapartment
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public int? InstructorID { get; set; }
        public Instructor Instructor { get; set; }
        public List<Course> Courses { get; set; }
    }
}
