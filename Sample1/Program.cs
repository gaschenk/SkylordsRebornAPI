using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SkylordsRebornAPI.Cardbase;

namespace Sample1
{
    class Program
    {
        private static readonly JsonSerializerSettings Settings = new() {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new StringEnumConverter()
            }
        };

        static void Main() {
            var x = CardService.HandleCardRequest(new List<Tuple<CardRequestProperty, string>>
            {
                new(CardRequestProperty.Name,"Gro"),
                new(CardRequestProperty.Defense,"1100")
            });
            foreach(var card in x) {
                Console.WriteLine(JsonConvert.SerializeObject(card, Settings));
            }
        }
    }
}