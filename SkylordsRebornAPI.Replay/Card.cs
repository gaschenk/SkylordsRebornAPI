using System.Globalization;
using System.Reflection;
using SkylordsRebornAPI.Cardbase.Cards;
using SkylordsRebornAPI.Replay.Data;
using Rarity = SkylordsRebornAPI.Replay.Data.Rarity;

namespace SkylordsRebornAPI.Replay
{
    public abstract class Card
    {
        protected Card(CultureInfo cultureInfo) => Id = GetType().GetCustomAttribute<CardFinderAttribute>().Id;

        public abstract string Name { get; protected set; }
        private uint Id { get; init; }
        public abstract string Description { get; protected set; }
        public abstract Rarity Rarity { get; protected set; }
        public abstract ushort[] Energy { get; protected set; }
        public abstract CardType CardType { get; protected set; }
        public abstract OrbColor[] OrbColors { get; protected set; }
    }
}