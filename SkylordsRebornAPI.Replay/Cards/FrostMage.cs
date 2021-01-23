using System.Globalization;
using System.Linq;
using System.Reflection;
using SkylordsRebornAPI.Cardbase.Cards;
using SkylordsRebornAPI.Replay.Data;
using Rarity = SkylordsRebornAPI.Replay.Data.Rarity;

namespace SkylordsRebornAPI.Replay.Cards
{
    [CardFinder(34287)]
    public sealed class FrostMage : Card
    {
        public FrostMage(CultureInfo cultureInfo) : base(cultureInfo)
        {
            Rarity = Rarity.Uncommon;
            Energy = new ushort[] {60};
            CardType = CardType.Creature;
            OrbColors = new[] {OrbColor.Frost};
        }

        public override string Name { get; protected set; }
        public override string Description { get; protected set; }
        public override Rarity Rarity { get; protected set; }
        public override ushort[] Energy { get; protected set; }
        public override CardType CardType { get; protected set; }
        public override OrbColor[] OrbColors { get; protected set; }
    }
}