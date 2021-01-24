using System;

namespace SkylordsRebornAPI.Replay
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class CardFinderAttribute : Attribute
    {
        public CardFinderAttribute(uint id)
        {
            Id = id;
        }

        public uint Id { get; set; }
    }
}