namespace HellDiver2_API2DB.V1_Objects {
    internal class steamData : Database_Record {
        public required string id { get; set; }                //Provided by API
        public required string title { get; set; }
        public required string url { get; set; }
        public required string author { get; set; }
        public required string content { get; set; }
        public required DateTime publishedAt { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/api/v1/steam";
        public override bool Equals(object? obj) {
            if (obj is not steamData data) {
                return false;
            }
            if (
               id == data.id
            && title == data.title
            && author == data.author
            && publishedAt == data.publishedAt
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
