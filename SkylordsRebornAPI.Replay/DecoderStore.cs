using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using SkylordsRebornAPI.Replay.ReplayKeys;

namespace SkylordsRebornAPI.Replay
{
    public class DecoderStore
    {
        private Dictionary<Data.ReplayKeys, ConstructorInfo> _decoders;

        public DecoderStore()
        {
            DiscoverDecoders();
        }

        private void DiscoverDecoders()
        {
            _decoders = Assembly.GetExecutingAssembly().GetTypes()
                .Select(x => (attr: x.GetCustomAttribute<KeyDecoderAttribute>(),
                    ctor: x.GetConstructor(new[] {typeof(BinaryReader), typeof(DecoderStore)})))
                .Where(x => x.attr != null && x.ctor != null)
                .ToDictionary(x => x.attr.Id, x => x.ctor);
        }

        public object Decode(Data.ReplayKeys key, BinaryReader data)
        {
            if (_decoders.TryGetValue(key, out var ctor))
            {
                var length = data.BaseStream.Position;
                var inst = ctor.Invoke(new object[] {data, this});
                return inst;
            }

            return null;
        }


        public Tuple<TimeSpan, Data.ReplayKeys, object> DecodeNext(BinaryReader reader)
        {
            try
            {
                var time = reader.ReadUInt32();
                var size = reader.ReadUInt32();
                var position = reader.BaseStream.Position;
                var key = (Data.ReplayKeys) reader.ReadInt32();

                if (!Enum.IsDefined(key))
                {
                    Debug.WriteLine($"Key doesn't exist {(int) key} @ length {reader.BaseStream.Position}");
                    var result =
                        new Tuple<TimeSpan, Data.ReplayKeys, object>(TimeSpan.FromMilliseconds(time) * 100, key,
                            HandleUnhandled(reader, (int) size)); //new Tuple<Data.ReplayKeys, object>(key,null);
                    reader.BaseStream.Position = position + size;
                    return result;
                }
                else
                {
                    Debug.WriteLine($"Key found {(int) key} @ length {reader.BaseStream.Position}");
                    var result = new Tuple<TimeSpan, Data.ReplayKeys, object>(TimeSpan.FromMilliseconds(time) * 100,
                        key, Decode(key, reader));
                    reader.BaseStream.Position = position + size;
                    return result;
                }
            }
            catch (EndOfStreamException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        private Unhandled HandleUnhandled(BinaryReader reader, int size)
        {
            return new()
            {
                Unknown = reader.ReadBytes(size)
            };
        }

        public List<Tuple<TimeSpan, Data.ReplayKeys, object>> DecodeFile(BinaryReader reader)
        {
            var results = new List<Tuple<TimeSpan, Data.ReplayKeys, object>>();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var result = DecodeNext(reader);
                if (result != null)
                    results.Add(result);
            }

            return results;
        }
    }
}