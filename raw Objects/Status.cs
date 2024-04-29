using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HD2_EFDatabase.raw_Objects {
    internal class Status {
        public int warId { get; set; }
        public long time { get; set; }
        public double impactMultiplier { get; set; }
        public long storyBeatId32 { get; set; }
        //TODO: implement previously used conversions
        [NotMapped]
        public required PlanetStatus[] planetStatus { get; set; }
        [NotMapped]
        public required planetAttacks[] planetAttacks { get; set; }
        [NotMapped]
        public required Campaign[] campaigns { get; set; }
        [NotMapped]
        public required JointOperation[] jointOperations { get; set; }
        [NotMapped]
        public required V1_Objects.EventData[] planetEvents { get; set; }


        public const bool CanIndex = false;
        public const string ApiEndpoint = "/raw/api/WarSeason/{WARID}/Status";
    }

    internal class PlanetStatus {
        [Key]
        public required int index { get; set; }
        public required int owner { get; set; }
        public required long health { get; set; }
        public required double regenPerSecond { get; set; }
        public required long players { get; set; }
    }
    internal class planetAttacks {
        [Key]
        public int id { get; set; }
    }
    internal class JointOperation {
        [Key]
        public required int id { get; set; }
        public required int planetIndex { get; set; }
        public required int hqNodeIndex { get; set; }
    }
}
