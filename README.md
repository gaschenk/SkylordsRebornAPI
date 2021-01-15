# SkylordsRebornAPI
A .Net API Implementation for Skylords Reborn API

## Example
```csharp
private static readonly JsonSerializerSettings Settings = new() {
    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters = {
            new StringEnumConverter()
        }
};

static void Main() {
    var x = SkylordsRebornAPI.Cardbase.CardService.GetCardsByName("Dread");
    foreach(var card in x) {
        Console.WriteLine(JsonConvert.SerializeObject(card, Settings));
    }
}
```
