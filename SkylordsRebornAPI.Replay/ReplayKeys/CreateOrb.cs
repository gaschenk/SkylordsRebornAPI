using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.CreateOrb)]
    public class CreateOrb
    {
        public CreateOrb(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            Unit = reader.ReadUInt32();
            Color = reader.ReadByte();
        }

        public byte Color { get; set; }

        public uint Unit { get; set; }

        public uint Source { get; set; }
    }
}