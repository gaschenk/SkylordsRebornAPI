using System;
using System.Collections.Generic;

namespace SkylordsRebornAPI.Replay.Data
{
    public class Replay
    {
        public List<MatrixEntry> Matrix { get; set; }
        public ulong HostPlayerId { get; set; }
        public TimeSpan PlayTime { get; set; }
        public uint ReplayRevision { get; set; }
        public string MapPath { get; set; }
        public List<Team> Teams { get; set; }
        public List<Tuple<TimeSpan, ReplayKeys, object>> ReplayKeys { get; set; }
        public uint GameVersion { get; set; }
        public byte[] UnknownMapData { get; set; }
    }
}