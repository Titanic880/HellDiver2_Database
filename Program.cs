using HellDiver2_API2DB.EntFramework;
using HellDiver2_API2DB.V1_Objects;
using Newtonsoft.Json;

namespace HellDiver2_API2DB {
    internal class Program {
        public static Config? UserConfig { get; private set; } = GetConfig();
        static void Main() {
            while (UserConfig == null || UserConfig == new Config()) {
                Console.WriteLine($"Empty config has been generated at: {Directory.GetCurrentDirectory()}/Config.json, populate the data before continuing...");
                Console.ReadLine();
                Thread.Sleep(10000);
                UserConfig = GetConfig();
            }
            //Main Update Loop
            while (true) {
                Console.WriteLine($"[{DateTime.UtcNow}] New Assignments: {DB_Logic.AddAssignmentData(JsonConvert.DeserializeObject<assignmentData[]>(API.CallAPI(assignmentData.apiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}] New Planets: {DB_Logic.AddPlanets(JsonConvert.DeserializeObject<Planet[]>(API.CallAPI(Planet.apiEndpoint).Replace(@"""event""",@"""events"""))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}] New Campaigns: {DB_Logic.AddCampaign2(JsonConvert.DeserializeObject<Campaign2[]>(API.CallAPI(Campaign2.apiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}] New Dispatches: {DB_Logic.AddDispatch(JsonConvert.DeserializeObject<Dispatch[]>(API.CallAPI(Dispatch.apiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}] New steamData: {DB_Logic.AddsteamData(JsonConvert.DeserializeObject<steamData[]>(API.CallAPI(steamData.apiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}] New WarInfo: {DB_Logic.AddWarInfo(JsonConvert.DeserializeObject<WarInfo>(API.CallAPI(WarInfo.apiEndpoint))!)}");
                Console.WriteLine($"[{DateTime.UtcNow}] Sleeping for: {UserConfig.SleepInterval_ms/1000} seconds");
                Thread.Sleep(UserConfig.SleepInterval_ms); //Sleep for 10 minutes between updates
            }
        }

        internal static Config? GetConfig() {
            if (File.Exists("Config.json")) {
                Config? ret = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));
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
                File.Create("Config.json").Close();
                File.WriteAllText("Config.json", JsonConvert.SerializeObject(new Config(),Formatting.Indented));
                return null;
            }
        }
        private static void DumpConfig(Config conf) {
            conf.Config_Ver = new Config().Config_Ver;
            File.WriteAllText("Config.json", JsonConvert.SerializeObject(conf,Formatting.Indented));
        }
    }
}
