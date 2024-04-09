namespace HellDiver2_API2DB.raw_Objects {
    internal class WarID {
        public required int id { get; set; }

        public const bool CanIndex = true;
        public const string apiEndpoint = "/raw/api/WarSeason/current/WarID";
    }
}
