using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.PveUnknownA8)]
    public class PveUnknownA8
    {
        public PveUnknownA8(BinaryReader reader, DecoderStore store)
        {
            Unknown = reader.ReadBytes(4);
            UnknownStructs = new List<byte[]>();
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++) UnknownStructs.Add(reader.ReadBytes(12));
        }

        public byte[] Unknown { get; set; }
        public List<byte[]> UnknownStructs { get; set; }
    }
}