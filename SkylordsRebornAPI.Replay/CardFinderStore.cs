using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SkylordsRebornAPI.Replay
{
    public class CardFinderStore
    {
        private Dictionary<uint, ConstructorInfo> _cardFinders;

        public CardFinderStore()
        {
            DiscoverCardFinders();
        }

        private void DiscoverCardFinders()
        {
            _cardFinders = Assembly.GetExecutingAssembly().GetTypes()
                .Select(x => (attr: x.GetCustomAttribute<CardFinderAttribute>(),
                    ctor: x.GetConstructor(new[] {typeof(CultureInfo)})))
                .Where(x => x.attr != null && x.ctor != null)
                .ToDictionary(x => x.attr.Id, x => x.ctor);
        }

        public Card FindCard(uint id, CultureInfo cultureInfo)
        {
            if (_cardFinders.TryGetValue(id, out var ctor))
            {
                var inst = ctor.Invoke(new object[] {cultureInfo});
                return inst as Card;
            }

            return null;
        }
    }
}