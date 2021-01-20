using System.Collections.Generic;

namespace SkylordsRebornAPI.Replay
{
    public struct Player
    {
        public List<Card> Cards { get; set; }
        public ulong PlayerId { get; set; }
    }
}