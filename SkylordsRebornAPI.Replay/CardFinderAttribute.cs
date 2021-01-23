using System;
using System.Diagnostics;
using System.IO;
using SkylordsRebornAPI.Replay.Data;
using SkylordsRebornAPI.Replay.ReplayKeys;

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