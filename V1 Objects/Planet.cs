using System.ComponentModel.DataAnnotations.Schema;
using HD2_EFDatabase.EntFramework;

namespace HD2_EFDatabase.V1_Objects {
    public class Planet : DatabaseRecord {
        public required int index { get; set; }                  //Provided by API 
        public required string name { get; set; }
        public required string sector { get; set; }
        public required long hash { get; set; }
        //TODO: Make this search up and use an already existing position if able.
        [ForeignKey("FK_Position_ID")]
        public required xyPosition position { get; set; }
        public long FK_Position_ID { get; set; }
        public required int[] waypoints { get; set; }
        public required long maxHealth { get; set; }
        public required long health { get; set; }
        public bool disabled { get; set; } = true;
        public required string initialOwner { get; set; }
        public required string currentOwner { get; set; }
        public required double regenPerSecond { get; set; }

        [ForeignKey("FK_Events_ID")]
        public EventData? events { get; set; }
        public long? FK_Events_ID { get; set; }
        [ForeignKey("FK_Stats_ID")]
        public Statistics? statistics { get; set; }
        public long? FK_Stats_ID { get; set; }

        public const bool CanIndex = true;
        public const string ApiEndpoint = "/api/v1/planets"; // /planet-events gets all active events

        public override bool Equals(object? obj) {
            if (obj is not Planet data) {
                return false;
            }
            //Dereference from the objects
            EventData? e1 = events;
            EventData? e2 = data.events;
            if(e1 == null && FK_Events_ID != null) {
                e1 = DbLogic.GetEvent((long)FK_Events_ID!);
            }
            if(e2 == null && data.FK_Events_ID != null) {
                e2 = DbLogic.GetEvent((long)FK_Events_ID!);
            }

            //Check against things that might change so that we can get reliable updates
            return index          == data.index
                && health         == data.health
                && regenPerSecond == data.regenPerSecond
                && e1?.id         == e2?.id;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
    public class xyPosition : DatabaseRecord {
        public required double x { get; set; }
        public required double y { get; set; }
    }
}
