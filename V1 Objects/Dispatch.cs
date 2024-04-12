namespace HellDiver2_API2DB.V1_Objects {
    internal class Dispatch : Database_Record {
        public required int id { get; set; }                //Provided by API
        public required DateTime published { get; set; }
        public required int type { get; set; }
        public required string message { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/api/v1/dispatches";

        public override bool Equals(object? obj) {
            if (obj is not Dispatch data) {
                return false;
            }
            if (
                id == data.id
            && type == data.type
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
