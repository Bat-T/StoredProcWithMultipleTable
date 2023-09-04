using System.ComponentModel.DataAnnotations;

namespace StoredProcTest.Entities
{
    public class Classes
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}
