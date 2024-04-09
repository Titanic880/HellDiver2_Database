namespace HellDiver2_API2DB.V1_Objects {
    internal class eventData : Database_Record {
        public required int id { get; set; }                    //Provided by API
        public required int eventType { get; set; }
        public required string faction { get; set; }
        public required Int64 health { get; set; }
        public required Int64 maxHealth { get; set; }
        public required DateTime startTime { get; set; }
        public required DateTime endTime { get; set; }
        public required Int64 campaignId { get; set; }
        public required int[] joinOperationIds { get; set; }
    }
}
