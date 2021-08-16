using System;
using System.Collections.Generic;

namespace EfFirstStep
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime? HireDate { get; set; }
        public List<CourseAssignment> CourseAssignments { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }
        public string Fullname => LastName.Trim() + " " + FirstMidName.Trim();
    }
}
