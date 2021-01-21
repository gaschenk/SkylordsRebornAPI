using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.Spell)]
    public class Spell
    {
        public ushort Card { get; set; }

        public ushort CardX { get; set; }
        public uint Source { get; set; }
        public byte Unknown { get; set; }
        public ushort CardC { get; set; }
        public ushort CardCx { get; set; }
        public int Charges { get; set; }
        public byte[] Unknown1 { get; set; }
        public uint Target { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public Spell(BinaryReader reader, DecoderStore store)
        {
            Card = reader.ReadUInt16();
            CardX = reader.ReadUInt16();
            Source = reader.ReadUInt32();
            Unknown = reader.ReadByte();
            CardC = reader.ReadUInt16();
            CardCx = reader.ReadUInt16();
            Charges = reader.ReadInt32();
            Unknown1 = reader.ReadBytes(5 + 8);
            Target = reader.ReadUInt32();
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
        }
    }
}