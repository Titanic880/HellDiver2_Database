using Microsoft.EntityFrameworkCore;
using HellDiver2_API2DB.V1_Objects;

namespace HellDiver2_API2DB.EntFramework {
    internal class DB_Context : DbContext {
        public DbSet<assignmentData> assignmentDatas { get; set; }
        public DbSet<taskData> taskDatas { get; set; }
        public DbSet<Reward> rewards { get; set; }
        public DbSet<Campaign2> campaign2s { get; set; } //V1 Campaign Data
        public DbSet<Dispatch> dispatches { get; set; }
        public DbSet<eventData> eventDatas { get; set; }
        public DbSet<Planet> planets { get; set; }
        public DbSet<xyPosition> xyPositions { get; set; }
        public DbSet<Statistics> statistics { get; set; }
        public DbSet<steamData> steamDatas { get; set; }
        public DbSet<WarInfo> warInfos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            Config Sys = Program.GetConfig()!;
            if (Sys == new Config()) {
                throw new Exception("Default config found.");
            }
            options.EnableSensitiveDataLogging();
            options.UseSqlServer($"Server={Sys.SQL_IP};Database={Sys.SQL_DB};User Id={Sys.SQL_ID};Password={Sys.SQL_PW};TrustServerCertificate=true");
        }
}
}
