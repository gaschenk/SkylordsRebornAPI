using System;
using System.IO;
using SkylordsRebornAPI.Replay.ReplayKeys;

namespace SkylordsRebornAPI.Replay
{
    public class GodClassReplayKeyHandler
    {
        internal AbstractReplayKey Handle(Data.ReplayKeys replayKey, BinaryReader reader)
        {
            return replayKey switch
            {
                Data.ReplayKeys.Attack => new Attack(reader),
                _ => throw new Exception($"Unknown Replay Key {(int) replayKey}")
            };
        }
    }
}