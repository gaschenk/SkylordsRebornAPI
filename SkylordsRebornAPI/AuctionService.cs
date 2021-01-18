using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkylordsRebornAPI.Auction;
using SkylordsRebornAPI.Cardbase;

namespace SkylordsRebornAPI
{
    public class AuctionService
    {
        private readonly string baseUrl = "https://auctions.backend.skylords.eu/";

        public AuctionEntry GetAuctionEntryInfo(int auctionID)
        {
            try
            {
                string url = $"{baseUrl}/api/auction/{auctionID}";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";

                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                if (text == String.Empty) return null;
                return JsonConvert.DeserializeObject<AuctionEntry>(text);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public List<AuctionEntry> GetAuctionEntriesOfPage(int page, int number, RequestBody requestBody)
        {
            try
            {
                string url = $"{baseUrl}/api/auctions/{page}/{number}";

                var webRequest = WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json; charset=utf-8";
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBody));
                webRequest.ContentLength = data.Length;

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                if (text == String.Empty) return null;
                return JsonConvert.DeserializeObject<List<AuctionEntry>>(text);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public uint GetAmountOfAuctions(RequestBody requestBody)
        {
            try
            {
                string url = $"{baseUrl}/api/auctions/count";

                var webRequest = WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json; charset=utf-8";
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBody));
                webRequest.ContentLength = data.Length;

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                return (JObject.Parse(text).GetValue("count") ?? -1).Value<uint>();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public AuctionCardIDRequestFormat GetCardInfo(CardId id)
        {
            try
            {
                string url = $"{baseUrl}api/cards/{(int) id}";

                var webRequest = WebRequest.Create(url);
                webRequest.ContentType = "application/json; charset=utf-8";
                var response = webRequest.GetResponse();
                string text;
                using (var sr = new StreamReader(response.GetResponseStream()!))
                {
                    text = sr.ReadToEnd();
                }

                if (text == "INVALID") return null;
                var deserializeObject = JsonConvert.DeserializeObject<AuctionCardIDRequestFormat>(text);
                return deserializeObject;
            }
            catch (Exception exception)
            {
                throw;
            }

            return null;
        }
    }
}