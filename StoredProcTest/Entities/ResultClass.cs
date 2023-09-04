using StoredProcTest.OutputModel;
using System.Security;

namespace StoredProcTest.Entities
{
    public record ResultClass
    {
        public IEnumerable<FirstTable> FirstTableItems { get; set; }
        public IEnumerable<SecondTable> SecondTableItems { get; set; }
    }
}
