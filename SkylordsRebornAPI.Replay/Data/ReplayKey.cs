using System;
using SkylordsRebornAPI.Replay.ReplayKeys;

namespace SkylordsRebornAPI.Replay.Data
{
    public struct ReplayKey
    {
        public ReplayKey(TimeSpan timeStamp, ReplayKeys key, object data)
        {
            TimeStamp = timeStamp;
            Key = key;
            KeyData = data;
        }

        public TimeSpan TimeStamp { get; set; }
        public ReplayKeys Key { get; set; }
        public object KeyData { get; set; }
    }
}