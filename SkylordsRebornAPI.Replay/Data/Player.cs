using System.Collections.Generic;

namespace SkylordsRebornAPI.Replay.Data
{
    public struct Player
    {
        public List<Card> Cards { get; set; }
        public ulong PlayerId { get; set; }
        public string Name { get; set; }
    }
}