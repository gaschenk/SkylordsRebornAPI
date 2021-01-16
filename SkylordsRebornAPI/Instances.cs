using SkylordsRebornAPI.Cardbase;

namespace SkylordsRebornAPI
{
    public class Instances
    {
        public static CardService CardService { get; } = new();
        public static MapService MapService { get; } = new();
    }
}