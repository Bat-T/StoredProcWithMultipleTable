using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoredProcTest.Entities
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Age { get; set; }

        [ForeignKey(nameof(Classes))]
        public int ClassId { get; set; }
        public Classes Classes { get; set; }
    }
}
