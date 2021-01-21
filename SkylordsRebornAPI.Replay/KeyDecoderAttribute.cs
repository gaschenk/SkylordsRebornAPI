using System;

namespace SkylordsRebornAPI.Replay
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class KeyDecoderAttribute : Attribute
    {
        public KeyDecoderAttribute(Data.ReplayKeys id)
        {
            Id = id;
        }

        public Data.ReplayKeys Id { get; set; }
    }
}