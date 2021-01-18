using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using SkylordsRebornAPI.Cardbase.Cards;

namespace SkylordsRebornAPI.Cardbase
{
    public class CardService
    {
        private readonly string baseUrl = "https://cardbase.skylords.eu/";

        public Card[] HandleCardRequest(List<Tuple<RequestProperty, string>> requestProperties)
        {
            var url = $"{baseUrl}Cards/GetCards?";
            for (var index = 0; index < requestProperties.Count; index++)
            {
                var requestProperty = requestProperties[index];
                url +=
                    $"{(index > 0 ? "&" : "")}{Enum.GetName(typeof(RequestProperty), requestProperty.Item1)}={requestProperty.Item2}";
            }

            var webRequest = WebRequest.Create(url);
            webRequest.ContentType = "application/json; charset=utf-8";
            var response = webRequest.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()!))
            {
                text = sr.ReadToEnd();
            }

            var deserializeObject = JsonConvert.DeserializeObject<APIWrap<Card>>(text);
            if (deserializeObject.Success != true)
                throw new Exception(
                    $"There has been an error with the API.\n{deserializeObject.Exception.Details}");
            var cards = deserializeObject.Result;
            return cards;
        }
    }
}