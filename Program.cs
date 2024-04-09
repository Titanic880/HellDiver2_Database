using HellDiver2_API2DB.EntFramework;
using HellDiver2_API2DB.V1_Objects;
using Newtonsoft.Json;

namespace HellDiver2_API2DB {
    internal class Program {
        public static Config? UserConfig { get; private set; } = GetConfig();
        static void Main() {
            if (UserConfig == null) {
                Console.WriteLine("Empty config has been generated, populate the data before continuing...");
                Console.ReadLine();
                return;
            }
            //Main Update Loop
            while (true) {
                Console.WriteLine($"New Assignments: {DB_Logic.AddAssignmentData(JsonConvert.DeserializeObject<assignmentData[]>(API.CallAPI(assignmentData.apiEndpoint))!)}");
                Console.WriteLine($"New Planets: {DB_Logic.AddPlanets(JsonConvert.DeserializeObject<Planet[]>(API.CallAPI(Planet.apiEndpoint))!)}");
                Console.WriteLine($"New Campaigns: {DB_Logic.AddCampaign2(JsonConvert.DeserializeObject<Campaign2[]>(API.CallAPI(Campaign2.apiEndpoint))!)}");
                Console.WriteLine($"New Dispatches: {DB_Logic.AddDispatch(JsonConvert.DeserializeObject<Dispatch[]>(API.CallAPI(Dispatch.apiEndpoint))!)}");
                Console.WriteLine($"New steamData: {DB_Logic.AddsteamData(JsonConvert.DeserializeObject<steamData[]>(API.CallAPI(steamData.apiEndpoint))!)}");
                Console.WriteLine($"New WarInfo: {DB_Logic.AddWarInfo(JsonConvert.DeserializeObject<WarInfo>(API.CallAPI(WarInfo.apiEndpoint))!)}");
               Thread.Sleep(600000); //Sleep for 10 minutes between updates
            }
        }

        private static Config? GetConfig() {
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
                File.WriteAllText("Config.json", JsonConvert.SerializeObject(new Config()));
                return null;
            }
        }
#if DEBUG
        private static void DumpConfig(Config conf) {
            //Debug to dump new config vars to file
            File.WriteAllText("Config.json", JsonConvert.SerializeObject(conf,Formatting.Indented));
        }
#endif
    }
}
