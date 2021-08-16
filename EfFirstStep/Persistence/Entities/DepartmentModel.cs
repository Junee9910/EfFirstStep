using System.ComponentModel.DataAnnotations.Schema;

namespace EfFirstStep
{
    [NotMapped]
    public class DepartmentModel
    {
        public string DepartmentName { get; set; }
        public int TotalStudent { get; set; }
        public int TotalCourse { get; set; }
    }
}
