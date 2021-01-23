using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.UnknownC1)]
    public class UnknownC1
    {
        public UnknownC1(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            var count = reader.ReadInt16();
            Units = new List<uint>();
            for (var i = 0; i < count; i++) Units.Add(reader.ReadUInt32());
            Unknown = reader.ReadBytes(8);
        }

        public byte[] Unknown { get; set; }

        public List<uint> Units { get; set; }

        public byte Color { get; set; }

        public uint Source { get; set; }
    }
}