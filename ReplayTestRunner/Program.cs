using SkylordsRebornAPI.Replay;

namespace ReplayTestRunner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ReplayReader reader = new();
            var x = reader.ReadReplay(
                @"C:\Users\Rai\Downloads\skylords_replay_analyzer\testreplays\PvE Mo dreaddy(NONX) 2009-08-06 20-58-51.pmv");
        }
    }
}