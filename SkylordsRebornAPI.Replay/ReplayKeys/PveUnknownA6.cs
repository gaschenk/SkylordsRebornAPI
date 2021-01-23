using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.PveUnknownA6)]
    public class PveUnknownA6
    {
        public PveUnknownA6(BinaryReader reader, DecoderStore store)
        {
            Unknown = reader.ReadBytes(12);
            UnknownStructs = new List<byte[]>();
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++) UnknownStructs.Add(reader.ReadBytes(12));
        }

        public byte[] Unknown { get; set; }
        public List<byte[]> UnknownStructs { get; set; }
    }
}