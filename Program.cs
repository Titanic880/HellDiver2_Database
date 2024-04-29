using HD2_EFDatabase.EntFramework;
using HD2_EFDatabase.V1_Objects;
using Newtonsoft.Json;

namespace HD2_EFDatabase {
    internal class Program {
        public static Config? userConfig { get; private set; } = GetConfig();
        private const string ConfigFile = "Config.json";
        private static void Main() {
            //User Config
            while (userConfig == null || userConfig == new Config()) {
                Console.WriteLine($"[{DateTime.UtcNow}][Info] Empty config has been generated at: {Directory.GetCurrentDirectory()}/Config.json, populate the data before continuing...");
                Console.ReadLine();
                Thread.Sleep(10000);
                userConfig = GetConfig();
            }

            //Database Creation Check
            if (userConfig.FirstRun) {
                DatabaseBuild();
                userConfig.FirstRun = false;
                DumpConfig(userConfig);
            }
            
            DateTime start = DateTime.Now;
            //Main Update Loop
            while (true) {
                Console.WriteLine($"[{DateTime.UtcNow}][Info] New Assignments: {DbLogic.AddAssignmentData(JsonConvert.DeserializeObject<assignmentData[]>(Api.GetCallApi(assignmentData.apiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}][Info] New Planets: {DbLogic.AddPlanets(JsonConvert.DeserializeObject<Planet[]>(Api.GetCallApi(Planet.ApiEndpoint).Replace(@"""event""",@"""events"""))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}][Info] New Campaigns: {DbLogic.AddCampaign2(JsonConvert.DeserializeObject<Campaign2[]>(Api.GetCallApi(Campaign2.ApiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}][Info] New Dispatches: {DbLogic.AddDispatch(JsonConvert.DeserializeObject<Dispatch[]>(Api.GetCallApi(Dispatch.ApiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}][Info] New steamData: {DbLogic.AddsteamData(JsonConvert.DeserializeObject<SteamData[]>(Api.GetCallApi(SteamData.ApiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}][Info] New WarInfo: {DbLogic.AddWarInfo(JsonConvert.DeserializeObject<WarInfo>(Api.GetCallApi(WarInfo.ApiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}][Info] Sleeping for: ~{(userConfig.SleepInterval_ms - (DateTime.Now - start).TotalMilliseconds) / 1000} seconds");
                Thread.Sleep((int)Math.Round(userConfig.SleepInterval_ms - (DateTime.Now - start).TotalMilliseconds,0)); //Sleep for 10 minutes between updates
                start = DateTime.Now;
            }
        }

        internal static Config? GetConfig() {
            if (File.Exists(ConfigFile)) {
                Config? ret = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigFile));
                if (ret == null) {
                    return null;
                } else if (ret.DefaultVal()) {
                    return null;
                }
                if (ret.Config_Ver < new Config().Config_Ver) {
                    DumpConfig(ret);
                }
                return ret;
            } else {
                File.Create(ConfigFile).Close();
                File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(new Config(),Formatting.Indented));
                return null;
            }
        }
        private static void DumpConfig(Config conf) {
            conf.Config_Ver = new Config().Config_Ver;
            File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(conf,Formatting.Indented));
        }

        private static void DatabaseBuild() {
            if (DbLogic.DataTableCheck()) {
                Console.WriteLine($"[{DateTime.UtcNow}][INFO]:First run & DataTables exist");
                userConfig!.FirstRun = false;
                return;
            }
            DbLogic.GenerateDatabase();
            Console.WriteLine(!DbLogic.DataTableCheck() //Double check if the tables were properly built
                ? $"[{DateTime.UtcNow}][ERROR]: Attempted to apply migrations to database, was unsuccessful."
                : $"[{DateTime.UtcNow}][Info]: Applied Migrations automatically.");
        }
    }
}
