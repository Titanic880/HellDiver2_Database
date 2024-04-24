namespace HD2_EFDatabase {
    internal static class Api {
        private static int _callsLeft = 1;
        private static readonly int API_SleepTime = Program.userConfig!.API_SleepTime_s;
        private static DateTime NextCallThres = DateTime.Now;


        internal static string GetCallApi(string uriEndpoint) {
            if (_callsLeft == 0) {
                Console.WriteLine($"[{DateTime.UtcNow}][Info] Sleeping for {API_SleepTime} seconds");
                Thread.Sleep(API_SleepTime * 1000);
                _callsLeft += 1;
                return GetCallApi(uriEndpoint);
            }
            HttpClient client = new() {
                BaseAddress = new Uri(Program.userConfig!.API_Endpoint)
            };
            client.DefaultRequestHeaders.Add("X-Application-Contact", Program.userConfig!.API_Contact);
            HttpResponseMessage? res;
            try {
                res = client.GetAsync(uriEndpoint).Result;
            } catch (Exception e) { 
                Console.WriteLine($"[{DateTime.UtcNow}][WARNING] Exception caught in API: {e}");
                Console.WriteLine($"[{DateTime.UtcNow}][Info] Retry will occour in 10 minutes...");
                Thread.Sleep(600000);
                return GetCallApi(uriEndpoint);
            }

            _callsLeft = int.Parse(res.Headers.GetValues("x-ratelimit-remaining").First());
            if (_callsLeft == 0) {
                NextCallThres = NextCallThres.AddSeconds(10000);
            }

            if (res.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new Exception("404: Content not found");
            } else if (res.StatusCode == System.Net.HttpStatusCode.TooManyRequests) {
                _ = int.TryParse(res.Headers.GetValues("Retry-After").First(), out int wait);
                Console.WriteLine($"[{DateTime.UtcNow}][WARNING] 429: Sleeping for {wait} seconds");
                Thread.Sleep(wait * 1000);
                _callsLeft += 1;
                return GetCallApi(uriEndpoint);
            }

            return res.Content.ReadAsStringAsync().Result;
        }
    }
}
