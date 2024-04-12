using HellDiver2_API2DB.V1_Objects;

namespace HD2_EFDatabase.V1_Objects {
    internal class Planet_Event : Planet {
        public int[] attacking { get; set; } = [];
        public new const bool CanIndex = false;
        public new const string apiEndpoint = "/api/v1/planet-events";

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
