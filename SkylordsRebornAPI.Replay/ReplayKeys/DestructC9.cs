using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.DestructC9)]
    public class DestructC9
    {
        public DestructC9(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            var count = reader.ReadInt32();
            Units = new List<uint>();
            for (var i = 0; i < count; i++) Units.Add(reader.ReadUInt32());
        }

        public List<uint> Units { get; set; }


        public uint Source { get; set; }
    }
}