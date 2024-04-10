using HellDiver2_API2DB.raw_Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB {
    public abstract class Database_Record {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PK_id { get; set; }
        public DateTime DataEntryTimeUTC { get; set; } = DateTime.UtcNow;
    }
}
