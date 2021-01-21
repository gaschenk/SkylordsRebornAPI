using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.Build)]
    public class Build
    {
        public ushort Card { get; set; }

        public ushort CardX { get; set; }
        public uint Source { get; set; }
        public ushort CardB { get; set; }
        public ushort CardBx { get; set; }
        public float X { get; set; }
        public float Z { get; set; }
        public float Y { get; set; }
        public byte[] Unknown { get; set; }
        public ushort CardC { get; set; }
        public ushort CardCx { get; set; }
        public int Charges { get; set; }

        public Build(BinaryReader reader, DecoderStore store)
        {
            Card = reader.ReadUInt16();
            CardX = reader.ReadUInt16();
            Source = reader.ReadUInt32();
            CardB = reader.ReadUInt16();
            CardBx = reader.ReadUInt16();
            X = reader.ReadSingle();
            Z = reader.ReadSingle();
            Y = reader.ReadSingle();
            Unknown = reader.ReadBytes(4);
            CardC = reader.ReadUInt16();
            CardCx = reader.ReadUInt16();
            Charges = reader.ReadInt32();
        }
    }
}