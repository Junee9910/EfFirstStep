using System.ComponentModel.DataAnnotations.Schema;

namespace EfFirstStep
{
    [NotMapped]
    public class StudentModel : Student
    {
        public string Grade { get; set; }
        public int TotalEnrollment{ get; set; }
        public int TotalStudent { get; set; }
    }
}
