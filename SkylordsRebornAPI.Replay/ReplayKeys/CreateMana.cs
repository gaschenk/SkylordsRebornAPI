using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.CreateMana)]
    public class CreateMana
    {
        public CreateMana(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            Unit = reader.ReadUInt32();
        }

        public uint Unit { get; set; }

        public uint Source { get; set; }
    }
}