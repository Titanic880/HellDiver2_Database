namespace HellDiver2_API2DB.V1_Objects {
    public class Statistics : Database_Record {
        public required Int64 missionsWon { get; set; }
        public required Int64 missionsLost { get; set; }
        public required Int64 missionTime { get; set; }
        public required Int64 terminidKills { get; set; }
        public required Int64 automatonKills { get; set; }
        public required Int64 illuminateKills { get; set; }
        public required Int64 bulletsFired { get; set; }
        public required Int64 bulletsHit { get; set; }
        public required Int64 timePlayed { get; set; }
        public required Int64 deaths { get; set; }
        public required Int64 revives { get; set; }
        public required Int64 friendlies { get; set; }
        public required Int64 missionSuccessRate { get; set; }
        public required Int64 accuracy { get; set; }
        public required Int64 playerCount { get; set; }
    }
}
