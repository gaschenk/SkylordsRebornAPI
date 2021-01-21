using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.UnknownBb)]
    public class UnknownBb
    {
        public UnknownBb(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            Unknown = reader.ReadBytes(17);
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Unknown2 = reader.ReadBytes(2);
        }

        public float Y { get; set; }

        public byte[] Unknown2 { get; set; }

        public float X { get; set; }

        public byte[] Unknown { get; set; }

        public uint Source { get; set; }
    }
}