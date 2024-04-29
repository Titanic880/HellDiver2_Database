using System.ComponentModel.DataAnnotations.Schema;

namespace HD2_EFDatabase.V1_Objects {
    internal class PlanetEvent : DatabaseRecord {
        [ForeignKey("FK_Planet_ID")]
        public required Planet planet { get; set; }
        public int FK_Planet_ID { get; set; }
        public int[] attacking { get; set; } = [];

        public const bool CanIndex = false;
        public const string ApiEndpoint = "/api/v1/planet-events";

        public override bool Equals(object? obj) {
            if (obj is not PlanetEvent data) {
                return false;
            }
            return base.Equals(data)
                && attacking == data.attacking;
        }
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}
