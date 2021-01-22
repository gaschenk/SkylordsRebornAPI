using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.LeaveGame)]
    public class LeaveGame
    {
        public LeaveGame(BinaryReader reader, DecoderStore store)
        {
            SourceUserId = reader.ReadUInt32();
        }

        public uint SourceUserId { get; set; }
    }
}