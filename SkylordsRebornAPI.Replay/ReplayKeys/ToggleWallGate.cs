using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.ToggleWallGate)]
    public class ToggleWallGate
    {
        public ToggleWallGate(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            Unknown = reader.ReadBytes(4);
        }

        public byte[] Unknown { get; set; }

        public uint Source { get; set; }
    }
}