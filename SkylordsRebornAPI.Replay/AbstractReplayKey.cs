using System;
using System.IO;

namespace SkylordsRebornAPI.Replay
{
    public class AbstractReplayKey
    {
        protected AbstractReplayKey(BinaryReader reader)
        {
            ReadData(reader);
        }

        private void ReadData(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}