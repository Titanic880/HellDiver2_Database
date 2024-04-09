using Microsoft.EntityFrameworkCore;
using HellDiver2_API2DB.V1_Objects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Reflection;


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
            options.EnableSensitiveDataLogging();
            options.UseSqlServer($"Server=127.0.0.1;Database=APIDBProj;User Id=sa;Password=SQLTestServer01!;TrustServerCertificate=true");
            //options.UseSqlServer($"Server={Program.UserConfig!.SQL_IP};Database={Program.UserConfig!.SQL_DB};User Id={Program.UserConfig!.SQL_ID};Password={Program.UserConfig!.SQL_PW};TrustServerCertificate=true");
        }
}
}
