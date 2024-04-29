namespace HD2_EFDatabase.V1_Objects {
    public class SteamData : DatabaseRecord {
        public required string id { get; set; }                //Provided by API
        public required string title { get; set; }
        public required string url { get; set; }
        public required string author { get; set; }
        public required string content { get; set; }
        public required DateTime publishedAt { get; set; }

        public const bool CanIndex = true;
        public const string ApiEndpoint = "/api/v1/steam";
        public override bool Equals(object? obj) {
            if (obj is not SteamData data) {
                return false;
            }
            return id          == data.id
                && title       == data.title
                && author      == data.author
                && publishedAt == data.publishedAt;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
