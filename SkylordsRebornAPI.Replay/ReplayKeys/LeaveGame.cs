using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.LeaveGame)]
    public class LeaveGame
    {
        public LeaveGame(BinaryReader reader, DecoderStore store)
        {
            SourceUserId = reader.ReadUInt32();
            Unknown = reader.ReadBytes(4);
        }

        public uint SourceUserId { get; set; }
        public byte[] Unknown { get; set; }
    }
}