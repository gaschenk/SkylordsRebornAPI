using System.Collections.Generic;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    public class Attack : AbstractReplayKey
    {
        public Attack(BinaryReader reader) : base(reader)
        {
            ReadData(reader);
        }

        public uint Source { get; set; }
        public List<uint> Units { get; set; }
        public uint Target { get; set; }

        private void ReadData(BinaryReader reader)
        {
            Source = reader.ReadUInt32();
            var unitCount = reader.ReadInt16();
            Units = new List<uint>();
            for (var i = 0; i < unitCount; i++) Units.Add(reader.ReadUInt32());

            reader.ReadBytes(5);

            //Value1
            reader.ReadUInt32();

            //Value2
            reader.ReadUInt32();
            Target = reader.ReadUInt32();
        }
    }
}