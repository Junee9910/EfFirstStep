using System.ComponentModel.DataAnnotations.Schema;

namespace EfFirstStep
{
    [NotMapped]
    public class StudentModel
    {
        public string Grade { get; set; }
        public int TotalStudent { get; set; }
    }
}
