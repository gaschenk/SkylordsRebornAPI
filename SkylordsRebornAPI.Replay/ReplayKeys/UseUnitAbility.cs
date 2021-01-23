using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.UseUnitAbility)]
    public class UseUnitAbility
    {
        public UseUnitAbility(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            SourceUnit = reader.ReadUInt32();
            Card = reader.ReadUInt16();
            CardX = reader.ReadUInt16();
            Unknown = reader.ReadBytes(5);
            Duration = reader.ReadUInt32();
            Unknown1 = reader.ReadBytes(4);
            Target = reader.ReadUInt32();
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
        }

        public ushort Card { get; set; }

        public ushort CardX { get; set; }
        public uint Source { get; set; }
        public uint SourceUnit { get; set; }

        public float Y { get; set; }

        public float X { get; set; }

        public byte[] Unknown1 { get; set; }

        public byte[] Unknown { get; set; }

        public uint Duration { get; set; }

        public uint Target { get; set; }
    }
}