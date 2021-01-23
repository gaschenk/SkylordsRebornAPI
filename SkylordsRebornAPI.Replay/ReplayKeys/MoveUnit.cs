using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace SkylordsRebornAPI.Replay.ReplayKeys
{
    [KeyDecoder(Data.ReplayKeys.Move)]
    public class MoveUnit
    {
        public MoveUnit(BinaryReader reader, DecoderStore store)
        {
            Source = reader.ReadUInt32();
            var count = reader.ReadUInt16();
            Units = new List<uint>();
            for (var i = 0; i < count; i++) Units.Add(reader.ReadUInt32());
            var newcount = reader.ReadUInt16();
            Debug.WriteLine(newcount);
            Debug.WriteLine(newcount);
            Positions = new List<PointF>();
            for (var i = 0; i < newcount; i++)
                Positions.Add(new PointF(reader.ReadSingle(), reader.ReadSingle()));

            Unknown = reader.ReadBytes(6);
        }

        public List<PointF> Positions { get; set; }

        public byte[] Unknown { get; set; }

        public List<uint> Units { get; set; }

        public uint Source { get; set; }
    }
}