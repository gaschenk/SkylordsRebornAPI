﻿using System;
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
        public List<Tuple<ReplayKeys, object>> ReplayKeys { get; set; }
    }

    public class MatrixEntry
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte Z { get; set; }
        
    }
}