using System.IO;
using Newtonsoft.Json;
using SkylordsRebornAPI.Replay;

namespace ReplayTestRunner
{
    internal class Program
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            Formatting = Formatting.Indented,
            DateParseHandling = DateParseHandling.None
        };

        private static void Main(string[] args)
        {
            ReplayReader reader = new();
            var x = reader.ReadReplay(
                @"C:\Users\Rai\Downloads\skylords_replay_analyzer\testreplays\autosave.pmv");
            File.WriteAllText(@".\output.txt", JsonConvert.SerializeObject(x, Settings));
        }
    }
}