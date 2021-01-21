using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.AbilityC3)]
    public class AbilityC3
    {
        public AbilityC3(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            Unit = reader.ReadUInt32();
            Unknown = reader.ReadBytes(4);
        }

        public byte[] Unknown { get; set; }

        public uint Unit { get; set; }

        public uint Source { get; set; }
    }
}