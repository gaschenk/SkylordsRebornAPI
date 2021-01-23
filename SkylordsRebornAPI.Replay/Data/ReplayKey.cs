using System;
using System.Collections.Generic;
using SkylordsRebornAPI.Replay.ReplayKeys;

namespace SkylordsRebornAPI.Replay.Data
{
    public class ReplayKey
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
        public List<ReplayKey> SubKey { get; set; }
    }
}