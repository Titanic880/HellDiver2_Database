namespace HD2_EFDatabase.V1_Objects {
    public class EventData : DatabaseRecord {
        public required int id { get; set; }                    //Provided by API
        public required int eventType { get; set; }
        public required string faction { get; set; }
        public required long health { get; set; }
        public required long maxHealth { get; set; }
        public required DateTime startTime { get; set; }
        public required DateTime endTime { get; set; }
        public required long campaignId { get; set; }
        public required int[] joinOperationIds { get; set; } = [];
    }
}
