using System.IO;
using SkylordsRebornAPI.Replay.Data;

namespace SkylordsRebornAPI.Replay
{
    public class GodclassActionShit
    {
        internal AbstractAction Handle(Actions action, BinaryReader reader)
        {
            return action switch
            {
                Actions.Attack => new ActionAttack(reader),
                _ => null
            };
        }
    }
}