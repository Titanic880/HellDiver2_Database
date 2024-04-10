namespace HellDiver2_API2DB {
    public static class API {
        private static int CallsLeft = 1;
        private static readonly int API_SleepTime = Program.UserConfig!.API_SleepTime_s;
        private static DateTime NextCallThres = DateTime.Now;


        internal static string CallAPI(string uriEndpoint) {
            if (CallsLeft == 0) {
                Console.WriteLine($"[{DateTime.UtcNow}] Sleeping for {API_SleepTime} seconds");
                Thread.Sleep(API_SleepTime * 1000);
                CallsLeft += 1;
                return CallAPI(uriEndpoint);
            }
            HttpClient client = new() {
                BaseAddress = new Uri(Program.UserConfig!.API_Endpoint)
            };
            client.DefaultRequestHeaders.Add("X-Application-Contact", Program.UserConfig!.API_Contact);
            HttpResponseMessage? res;
            try {
                res = client.GetAsync(uriEndpoint).Result;
            } catch (Exception e) { 
                Console.WriteLine($"[{DateTime.UtcNow}] Exception caught in API: {e}");
                Console.WriteLine("Retry will occour in 10 minutes...");
                Thread.Sleep(600000);
                return CallAPI(uriEndpoint);
            }

            CallsLeft = int.Parse(res.Headers.GetValues("x-ratelimit-remaining").First());
            if (CallsLeft == 0) {
                NextCallThres = NextCallThres.AddSeconds(10000);
            }

            if (res.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("404: Content not found");
            } else if (res.StatusCode == System.Net.HttpStatusCode.TooManyRequests) {
                _ = int.TryParse(res.Headers.GetValues("Retry-After").First(), out int wait);
                Console.WriteLine($"[{DateTime.UtcNow}] 429: Sleeping for {wait} seconds");
                Thread.Sleep(wait * 1000);
                CallsLeft += 1;
                return CallAPI(uriEndpoint);
            }

            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
