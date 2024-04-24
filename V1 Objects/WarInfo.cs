
using System.ComponentModel.DataAnnotations.Schema;

namespace HD2_EFDatabase.V1_Objects {
    public class WarInfo : DatabaseRecord {
        public required DateTime started { get; set; }
        public required DateTime ended { get; set; }
        public required DateTime now { get; set; }
        public required string clientVersion { get; set; }
        public required string[] factions { get; set; }
        public required double impactMultiplier { get; set; }
        [ForeignKey("FK_Stats_ID")]
        public required Statistics statistics { get; set; }
        public long? FK_Stats_ID { get; set; }
        public const bool CanIndex = false;
        public const string ApiEndpoint = "/api/v1/war";

        public override bool Equals(object? obj) {
            if (obj is not WarInfo data) {
                return false;
            }
            return started          == data.started
                && ended            == data.ended
                && now              == data.now
                && clientVersion    == data.clientVersion
                && impactMultiplier == data.impactMultiplier
                && statistics       == data.statistics;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
