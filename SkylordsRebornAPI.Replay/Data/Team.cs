using System.Collections.Generic;

namespace SkylordsRebornAPI.Replay
{
    public struct Team
    {
        public string Name { get; set; }
        public int TeamId { get; set; }

        public List<Player> Players { get; set; }
    }
}