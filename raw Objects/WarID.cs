namespace HD2_EFDatabase.raw_Objects {
    internal class WarID {
        public required int id { get; set; }

        public const bool CanIndex = true;
        public const string ApiEndpoint = "/raw/api/WarSeason/current/WarID";
    }
}
