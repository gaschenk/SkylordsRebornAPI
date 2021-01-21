using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.UnknownC2)]
    public class UnknownC2
    {
        public UnknownC2(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            Unit = reader.ReadUInt32();
            Card = reader.ReadUInt16();
            CardX = reader.ReadUInt16();
        }

        public ushort Card { get; set; }

        public ushort CardX { get; set; }

        public uint Unit { get; set; }

        public uint Source { get; set; }
    }
}