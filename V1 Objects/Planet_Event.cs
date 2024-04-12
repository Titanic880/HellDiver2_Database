using System.ComponentModel.DataAnnotations.Schema;
using HellDiver2_API2DB.V1_Objects;
using HellDiver2_API2DB;

namespace HD2_EFDatabase.V1_Objects {
    internal class Planet_Event : Database_Record {
        [ForeignKey("FK_Planet_ID")]
        public required Planet planet { get; set; }
        public int FK_Planet_ID { get; set; }
        public int[] attacking { get; set; } = [];

        public const bool CanIndex = false;
        public const string apiEndpoint = "/api/v1/planet-events";

        public override bool Equals(object? obj) {
            if (obj is not Planet_Event data) {
                return false;
            }
            if (
               base.Equals(data)
            && attacking == data.attacking
            ) {
                return true;
            }
            return false;
        }
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}
