﻿using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.UnknownA1)]
    public class UnknownA1
    {
        public byte[] Unknown { get; set; }

        public UnknownA1(BinaryReader reader, DecoderStore store)
        {
            Unknown = reader.ReadBytes(8);
        }
    }
}