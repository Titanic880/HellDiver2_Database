using HD2_EFDatabase.V1_Objects;
using Microsoft.EntityFrameworkCore;

namespace HD2_EFDatabase.EntFramework {
    internal class DbContext : Microsoft.EntityFrameworkCore.DbContext {
        public DbSet<assignmentData> assignmentDatas { get; set; }
        public DbSet<taskData> taskDatas { get; set; }
        public DbSet<Reward> rewards { get; set; }
        public DbSet<Campaign2> campaign2s { get; set; } //V1 Campaign Data
        public DbSet<Dispatch> dispatches { get; set; }
        public DbSet<EventData> eventDatas { get; set; }
        public DbSet<Planet> planets { get; set; }
        public DbSet<xyPosition> xyPositions { get; set; }
        public DbSet<Statistics> statistics { get; set; }
        public DbSet<SteamData> steamDatas { get; set; }
        public DbSet<WarInfo> warInfos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            Config sys = Program.GetConfig()!;
            if (sys == new Config()) {
                throw new Exception("Default config found.");
            }
            options.EnableSensitiveDataLogging();
            options.UseSqlServer($"Server={sys.SQL_IP};Database={sys.SQL_DB};User Id={sys.SQL_ID};Password={sys.SQL_PW};TrustServerCertificate=true");
        }
    }
}