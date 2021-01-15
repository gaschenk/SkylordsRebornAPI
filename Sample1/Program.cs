using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SkylordsRebornAPI;
using SkylordsRebornAPI.Cardbase.Cards;

namespace Sample1
{
    class Program
    {
        private static readonly JsonSerializerSettings Settings = new() {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            Formatting = Formatting.Indented,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new StringEnumConverter()
            }
        };

        static void Main() {
            var x = Instances.CardService.HandleCardRequest(new List<Tuple<RequestProperty, string>>
            {
                new(RequestProperty.Name,"Gro"),
                new(RequestProperty.Defense,"1100")
            });
            foreach(var card in x) {
                Console.WriteLine(JsonConvert.SerializeObject(card, Settings));
            }
        }
    }
}