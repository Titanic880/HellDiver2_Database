using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HellDiver2_API2DB.raw_Objects {
    internal class Status {
        public int warId { get; set; }
        public Int64 time { get; set; }
        public double impactMultiplier { get; set; }
        public Int64 storyBeatId32 { get; set; }
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
        public required V1_Objects.eventData[] planetEvents { get; set; }


        public const bool CanIndex = false;
        public const string apiEndpoint = "/raw/api/WarSeason/{WARID}/Status";
    }

    internal class PlanetStatus {
        [Key]
        public required int index { get; set; }
        public required int owner { get; set; }
        public required Int64 health { get; set; }
        public required double regenPerSecond { get; set; }
        public required Int64 players { get; set; }
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
