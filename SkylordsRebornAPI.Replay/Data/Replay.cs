using System;
using System.Collections.Generic;

namespace SkylordsRebornAPI.Replay
{
    public class Replay
    {
        //public MatrixEntry[] Matrix { get; set; }
        public ulong HostPlayerId { get; set; }
        public TimeSpan PlayTime { get; set; }
        public uint ReplayFormatVersion { get; set; }
        public string MapPath { get; set; }
        public List<Team> Teams { get; set; }
    }
}