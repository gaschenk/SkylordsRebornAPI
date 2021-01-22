using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.LineSpell)]
    public class LinearSpell
    {
        public ushort Card { get; set; }

        public ushort CardX { get; set; }
        public uint Source { get; set; }
        public ushort CardC { get; set; }
        public ushort CardCx { get; set; }
        public byte Charges { get; set; }
        public byte[] Unknown1 { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public byte[] Unknown2 { get; set; }

        public float X2 { get; set; }

        public float Y2 { get; set; }

        public LinearSpell(BinaryReader reader, DecoderStore store)
        {
            Card = reader.ReadUInt16();
            CardX = reader.ReadUInt16();
            Source = reader.ReadUInt32();
            Charges = reader.ReadByte();
            CardC = reader.ReadUInt16();
            CardCx = reader.ReadUInt16();
            Unknown1 = reader.ReadBytes(18);
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Unknown2 = reader.ReadBytes(12);
            X2 = reader.ReadSingle();
            Y2 = reader.ReadSingle();
        }
    }
}