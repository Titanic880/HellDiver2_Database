namespace HellDiver2_API2DB {
    internal class Config {
        public int Config_Ver = 2;
        public string API_Contact = "YOURNAMEHERE";
        public string API_Endpoint = "https://helldivers-2-dotnet.fly.dev";
        public int API_SleepTime_s = 15;
        public int SleepInterval_ms = 600000;

        public string SQL_IP = "127.0.0.1";
        public string SQL_DB = "newDb";
        public string SQL_ID = "sa";
        public string SQL_PW = "PASSWORD";
        public bool DefaultVal() {
            if(
                API_Contact == new Config().API_Contact ||
                API_Contact == "" ||
                SQL_IP == "" ||
                SQL_DB == "" ||
                SQL_ID == "" ||
                SQL_PW == "" || 
                SQL_PW == "PASSWORD"
                ) {
                return true;
            }
            return false;
        }
    }
}