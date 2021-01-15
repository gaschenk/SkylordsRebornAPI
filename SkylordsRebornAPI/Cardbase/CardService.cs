using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using SkylordsRebornAPI.Cardbase.Cards;

namespace SkylordsRebornAPI.Cardbase
{
    public static class CardService
    {
        private static string baseUrl = "https://cardbase.skylords.eu/";

        public static Card[] GetCardsByName(string name)
        {
            var webRequest = WebRequestCreator($"Cards/GetCards?Name={name}");
            var response = webRequest.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()!))
            {
                text = sr.ReadToEnd();
            }

            var cards = JsonConvert.DeserializeObject<APIWrap<Card>>(text).Result;
            return cards;
        }

        public static Card[] GetCardsByCardType(CardType type)
        {
            var webRequest = WebRequestCreator($"Cards/GetCards?CardType={(int) type}");
            var response = webRequest.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()!))
            {
                text = sr.ReadToEnd();
            }

            var cards = JsonConvert.DeserializeObject<APIWrap<Card>>(text).Result;
            return cards;
        }

        public static Card[] GetCardsByCategory(string category)
        {
            var webRequest = WebRequestCreator($"Cards/GetCards?Category={category}");
            var response = webRequest.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()!))
            {
                text = sr.ReadToEnd();
            }

            var cards = JsonConvert.DeserializeObject<APIWrap<Card>>(text).Result;
            return cards;
        }

        public static Card[] GetCardsByCost(int cost)
        {
            var webRequest = WebRequestCreator($"Cards/GetCards?Cost={cost}");
            var response = webRequest.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()!))
            {
                text = sr.ReadToEnd();
            }

            var cards = JsonConvert.DeserializeObject<APIWrap<Card>>(text).Result;
            return cards;
        }

        private static WebRequest WebRequestCreator(string additionalPath)
        {
            var webRequest = WebRequest.Create($"{baseUrl}{additionalPath}");
            webRequest.ContentType = "application/json; charset=utf-8";
            return webRequest;
        }

        public static Card[] HandleCardRequest(List<Tuple<CardRequestProperty, String>> requestProperties)
        {
            string url = $"{baseUrl}Cards/GetCards?";
            for (var index = 0; index < requestProperties.Count; index++)
            {
                var requestProperty = requestProperties[index];
                url +=
                    $"{(index > 0 ? "&" : "")}{Enum.GetName(typeof(CardRequestProperty), requestProperty.Item1)}={requestProperty.Item2}";
            }

            var webRequest = WebRequest.Create(url);
            webRequest.ContentType = "application/json; charset=utf-8";
            var response = webRequest.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()!))
            {
                text = sr.ReadToEnd();
            }

            var cards = JsonConvert.DeserializeObject<APIWrap<Card>>(text).Result;
            return cards;
        }
    }

    public enum CardRequestProperty
    {
        Name,
        Cost,
        CardType,
        Category,
        Defense,
    }
}