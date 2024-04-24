namespace HD2_EFDatabase.V1_Objects {
    public class Statistics : DatabaseRecord {
        public required long missionsWon { get; set; }
        public required long missionsLost { get; set; }
        public required long missionTime { get; set; }
        public required long terminidKills { get; set; }
        public required long automatonKills { get; set; }
        public required long illuminateKills { get; set; }
        public required long bulletsFired { get; set; }
        public required long bulletsHit { get; set; }
        public required long timePlayed { get; set; }
        public required long deaths { get; set; }
        public required long revives { get; set; }
        public required long friendlies { get; set; }
        public required long missionSuccessRate { get; set; }
        public required long accuracy { get; set; }
        public required long playerCount { get; set; }
    }
}
