using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.BuildWall)]
    public class BuildWall
    {
        public BuildWall(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            UnknownValue = reader.ReadUInt32();
            if (UnknownValue != 2409)
                Unknown = reader.ReadByte();
        }

        public uint UnknownValue { get; set; }

        public byte Unknown { get; set; }

        public uint Source { get; set; }
    }
}