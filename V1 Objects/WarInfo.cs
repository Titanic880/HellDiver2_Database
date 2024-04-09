
using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB.V1_Objects {
    internal class WarInfo : Database_Record {
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
        public const string apiEndpoint = "/api/v1/war";

        public override bool Equals(object? obj) {
            if (obj is not WarInfo data) {
                return false;
            }
            if (
               started == data.started
            && ended == data.ended
            && now == data.now
            && clientVersion == data.clientVersion
            && impactMultiplier == data.impactMultiplier
            && statistics == data.statistics
            ) {
                return true;
            }

            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
