using System.Globalization;
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

        public override Rarity Rarity { get; protected set; }
        public override ushort[] Energy { get; protected set; }
        public override CardType CardType { get; protected set; }
        public override OrbColor[] OrbColors { get; protected set; }
    }
}