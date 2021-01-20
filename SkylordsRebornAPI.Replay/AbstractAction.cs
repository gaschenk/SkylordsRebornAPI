using System.IO;

namespace SkylordsRebornAPI.Replay
{
    public class AbstractAction
    {
        protected AbstractAction(BinaryReader reader)
        {
            ReadData(reader);
        }

        private void ReadData(BinaryReader reader)
        {
            throw new System.NotImplementedException();
        }
    }
}