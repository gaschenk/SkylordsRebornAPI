using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkylordsRebornAPI.Auction;
using SkylordsRebornAPI.Exceptions;
using SkylordsRebornAPI.Leaderboards;

namespace SkylordsRebornAPI
{
    public class LeaderboardsService
    {
        private readonly string baseUrl = "https://leaderboards.backend.skylords.eu";

        public ulong? NextCachingRefresh()
        {
            try
            {
                string url = $"{baseUrl}/api/next-load";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";
                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                var value = (JObject.Parse(text).GetValue("in") ?? -1).Value<ulong>();
                return value;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public List<GenericData> GetMapByPvEMode(PvEModes pveMode)
        {
            try
            {
                string url = $"{baseUrl}/api/maps/{(int) pveMode}pve";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";
                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                var deserializeObject = JsonConvert.DeserializeObject<List<GenericData>>(text);
                return deserializeObject;
            }
            catch (Exception exception)
            {
                throw;
            }

            return null;
        }

        public List<GenericData> GetTimeRanges()
        {
            try
            {
                string url = $"{baseUrl}/api/ranges";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";
                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                var deserializeObject = JsonConvert.DeserializeObject<List<GenericData>>(text);
                return deserializeObject;
            }
            catch (Exception exception)
            {
                throw;
            }

            return null;
        }

        public List<GenericData> GetDifficulties()
        {
            try
            {
                string url = $"{baseUrl}/api/difficulties";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";
                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                var deserializeObject = JsonConvert.DeserializeObject<List<GenericData>>(text);
                return deserializeObject;
            }
            catch (Exception exception)
            {
                throw;
            }

            return null;
        }

        public uint GetPvELeaderboardCount(PvEModes pveMode, PlayerCount playerCount, int mapID, int month)
        {
            try
            {
                string url =
                    $"{baseUrl}/api/leaderboards/pve-count/{(int) pveMode}/{(int) playerCount}/{mapID}/{month}";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";

                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                return (JObject.Parse(text).GetValue("count") ?? -1).Value<uint>();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public List<MatchInfo> GetPvELeaderboard(PvEModes pveMode, PlayerCount playerCount, int mapID, int month,
            int page, int totalResults)
        {
            try
            {
                string url =
                    $"{baseUrl}/api/leaderboards/pve/{(int) pveMode}/{(int) playerCount}/{mapID}/{month}/{page}/{totalResults}";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";

                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                var deserializeObject = JsonConvert.DeserializeObject<List<MatchInfo>>(text);
                return deserializeObject;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public uint GetPVPLeaderboardCount(PvPModes pvpMode, int month)
        {
            try
            {
                string url = $"{baseUrl}/api/leaderboards/pvp-count/{(int) pvpMode}/{month}";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";

                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                return (JObject.Parse(text).GetValue("count") ?? -1).Value<uint>();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public List<PVPMatchInfo> GetPVPLeaderboard(PvPModes pvpMode, int month, int page, int totalResults)
        {
            try
            {
                string url = $"{baseUrl}/api/leaderboards/pvp/{(int) pvpMode}/{month}/{page}/{totalResults}";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";

                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                CheckBackendStatus(text);
                var deserializeObject = JsonConvert.DeserializeObject<List<PVPMatchInfo>>(text);
                return deserializeObject;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private void CheckBackendStatus(string responseText)
        {
            var state = JObject.Parse(responseText).GetValue("state");
            if (state != null)
            {
                var stateValue = state.Value<String>();
                throw stateValue switch
                {
                    "invalid" => new BackendInvalidException(),
                    "loading" => new BackendCachingException(),
                    _ => new BackendUnknownException($"stateValue: {stateValue}")
                };
            }
        }
    }
}