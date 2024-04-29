using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD2_EFDatabase {
    public abstract class DatabaseRecord {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PK_id { get; set; }
        public DateTime DataEntryTimeUTC { get; set; } = DateTime.UtcNow;
    }
}
