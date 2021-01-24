using System;
using System.Collections.Generic;

namespace SkylordsRebornAPI.Replay.Data
{
    public class ReplayKey
    {
        public ReplayKey(TimeSpan timeStamp, ReplayKeys key, object data)
        {
            TimeStamp = timeStamp;
            Key = key;
            KeyData = data;
            SubKey = new List<object>();
        }

        public TimeSpan TimeStamp { get; set; }
        public ReplayKeys Key { get; set; }
        public object KeyData { get; set; }
        public List<object> SubKey { get; set; }
    }
}