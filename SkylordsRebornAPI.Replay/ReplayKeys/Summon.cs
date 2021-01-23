using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.Summon)]
    public class Summon
    {
        public Summon(BinaryReader reader, DecoderStore store)
        {
            Card = reader.ReadUInt16();
            CardX = reader.ReadUInt16();
            Source = reader.ReadUInt32();
            Unknown = reader.ReadByte();
            CardC = reader.ReadUInt16();
            CardCx = reader.ReadUInt16();
            Charges = reader.ReadByte();
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Unknown1 = reader.ReadBytes(4);
        }

        public ushort Card { get; set; }

        public ushort CardX { get; set; }
        public uint Source { get; set; }
        public byte Unknown { get; set; }
        public ushort CardC { get; set; }
        public ushort CardCx { get; set; }
        public byte Charges { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public byte[] Unknown1 { get; set; }
    }
}