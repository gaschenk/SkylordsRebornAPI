using System.IO;
using System.Text.Json;
using SkylordsRebornAPI.Replay;

namespace ReplayTestRunner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ReplayReader reader = new();
            var x = reader.ReadReplay(
                @"C:\Users\Rai\Downloads\skylords_replay_analyzer\testreplays\autosave.pmv");
            File.WriteAllText(@".\output.txt",JsonSerializer.Serialize(x));
        }
    }
}