using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.StopUnit)]
    public class StopUnit
    {
        public StopUnit(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            var count = reader.ReadUInt16();
            Units = new List<uint>();
            for (var i = 0; i < count; i++) Units.Add(reader.ReadUInt32());
        }

        public List<uint> Units { get; set; }

        public uint Source { get; set; }
    }
}