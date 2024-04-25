using System.Net;

namespace HD2_EFDatabase {
    internal static class Api {
        private static int _callsLeft = 1;
        private static readonly int ApiSleepTime = Program.userConfig!.API_SleepTime_s;

        internal static string GetCallApi(string uriEndpoint) {
            while (true) {
                if (_callsLeft == 0) {
                    LogSleep($"[{DateTime.UtcNow}][Info] Sleeping for {ApiSleepTime} seconds", ApiSleepTime * 1000);
                    _callsLeft += 1;
                    continue;
                }

                HttpClient client = new() { BaseAddress = new Uri(Program.userConfig!.API_Endpoint) };
                client.DefaultRequestHeaders.Add("X-Application-Contact", Program.userConfig.API_Contact);
                HttpResponseMessage? res;

                try {
                    res = client.GetAsync(uriEndpoint).Result;
                }
                catch (Exception e) {
                    LogSleep(
                        $"[{DateTime.UtcNow}][WARNING] Exception caught in API: {e.Message}" +
                        $"\n[{DateTime.UtcNow}][Info] Retry will occour in 10 minutes...", 600000);
                    continue;
                }

                if (res.StatusCode == HttpStatusCode.RequestTimeout) {
                    Console.WriteLine();
                    LogSleep(
                        $"[{DateTime.UtcNow}][WARNING] API returned 408 (gateway time out), sleeping for 10 minutes..."
                      , 600000);
                    continue;
                }
                
                if (int.TryParse(res.Headers.GetValues("x-ratelimit-remaining").FirstOrDefault()
                       ,out _callsLeft) is false) {
                    _callsLeft = 1;
                }

                switch (res.StatusCode) {
                    case HttpStatusCode.OK:
                        return res.Content.ReadAsStringAsync().Result;
                    case HttpStatusCode.NotFound:
                        throw new Exception("404: Content not found");
                    case HttpStatusCode.TooManyRequests:
                        _ = int.TryParse(res.Headers.GetValues("Retry-After").First(), out int wait);
                        LogSleep($"[{DateTime.UtcNow}][WARNING] 429: Sleeping for {wait} seconds", wait * 1000);
                        _callsLeft += 1;
                        continue;
                    case HttpStatusCode.BadGateway:
                        LogSleep($"[{DateTime.UtcNow}][ERROR] 502: Sleeping for 10 minutes", ApiSleepTime * 1000);
                        _callsLeft += 1;
                        continue;
                    case HttpStatusCode.Forbidden:
                        LogSleep("How...?", 60000);
                        break;
                }
            }
        }

        private static void LogSleep(string message, int time) {
            Console.WriteLine(message);
            Thread.Sleep(time);
        }
    }
}
