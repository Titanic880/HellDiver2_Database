using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace HellDiver2_API2DB.V1_Objects {
    internal class Planet : Database_Record {
        public required int index { get; set; }                  //Provided by API 
        public required string name { get; set; }
        public required string sector { get; set; }
        public required Int64 hash { get; set; }
        [ForeignKey("FK_Position_ID")]
        public required xyPosition position { get; set; }
        public long FK_Position_ID { get; set; }
        public required int[] waypoints { get; set; }
        public required Int64 maxHealth { get; set; }
        public required Int64 health { get; set; }
        public bool disabled { get; set; } = true;
        public required string initialOwner { get; set; }
        public required string currentOwner { get; set; }
        public required double regenPerSecond { get; set; }
        [ForeignKey("FK_Events_ID")]
        public eventData? events { get; set; }
        public long? FK_Events_ID { get; set; }
        [ForeignKey("FK_Stats_ID")]
        public Statistics? statistics { get; set; }
        public long? FK_Stats_ID { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/api/v1/planets"; // /planet-events gets all active events

        public override bool Equals(object? obj) {
            if (obj is not Planet data) {
                return false;
            }
            if ( //Check against things that might change so that we can get reliable updates
               index == data.index
            && health == data.health
            && regenPerSecond == data.regenPerSecond
            ) {
                return true;
            }

            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
    public class xyPosition : Database_Record {
        public required double x { get; set; }
        public required double y { get; set; }
    }
}
