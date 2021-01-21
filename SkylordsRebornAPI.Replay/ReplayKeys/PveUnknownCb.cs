﻿using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.PveUnknownCb)]
    public class PveUnknownCb
    {
        public PveUnknownCb(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            var count = reader.ReadInt32();
            Units = new List<uint>();
            for (int i = 0; i < count; i++)
            {
                Units.Add(reader.ReadUInt32());
            }
        }

        public List<uint> Units { get; set; }


        public uint Source { get; set; }
    }
}