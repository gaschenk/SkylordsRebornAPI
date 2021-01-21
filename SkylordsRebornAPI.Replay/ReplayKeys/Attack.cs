using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.Attack)]
    public class Attack
    {
        public uint Source { get; set; }
        public List<uint> Units { get; set; }
        public uint Target { get; set; }
        public byte[] Unknown { get; set; }

        public Attack(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            var unitCount = reader.ReadInt16();
            Units = new List<uint>();
            for (var i = 0; i < unitCount; i++) Units.Add(reader.ReadUInt32());

            Unknown = reader.ReadBytes(5 + 8);

            //Value1
            //reader.ReadUInt32();

            //Value2
            //reader.ReadUInt32();
            Target = reader.ReadUInt32();
        }
    }
}