using System.ComponentModel.DataAnnotations.Schema;

namespace EfFirstStep
{
    [NotMapped]
    public class DepartmentModel : Dapartment
    {
        public int TotalStudent { get; set; }
        public int TotalCourse { get; set; }
    }
}
