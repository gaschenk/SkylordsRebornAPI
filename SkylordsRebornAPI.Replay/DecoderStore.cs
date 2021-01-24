using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using SkylordsRebornAPI.Replay.Data;
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


        public Data.ReplayKey DecodeNext(BinaryReader reader)
        {
            try
            {
                var time = reader.ReadUInt32();
                var size = reader.ReadUInt32();
                Debug.WriteLine("time: " + time);
                Debug.WriteLine("size: " + size);
                var position = reader.BaseStream.Position;
                var key = (Data.ReplayKeys) reader.ReadInt32();

                if (!Enum.IsDefined(key))
                {
                    Debug.WriteLine($"Key doesn't exist {(int) key} @ length {reader.BaseStream.Position}");
                    var result =
                        new ReplayKey(TimeSpan.FromMilliseconds(time) * 100, key,
                            HandleUnhandled(reader, (int) size)); //new Tuple<Data.ReplayKeys, object>(key,null);
                    reader.BaseStream.Position = position + size;
                    return result;
                }
                else
                {
                    Debug.WriteLine($"Key found {(int) key} @ length {reader.BaseStream.Position}");
                    var result = new ReplayKey(TimeSpan.FromMilliseconds(time) * 100,
                        key, Decode(key, reader));
                    var possibleLength = reader.BaseStream.Position - (position - size);
                    if (reader.BaseStream.Position > position + size)
                    {
                        Debug.WriteLine("Exceeded length by " + (possibleLength) + "bytes");
                    }
                    else if (reader.BaseStream.Position < position + size)
                    {
                        Debug.WriteLine("lost length by " + (possibleLength) + "bytes");
                        //result.SubKey.Add( DecodeSubNext(reader, (int)possibleLength,result));
                        //Debug.WriteLine(BitConverter.ToString(reader.ReadBytes((int) possibleLength)));
                        result.SubKey.Add(HandleUnhandled(reader, (int) possibleLength));
                    }

                    reader.BaseStream.Position = position + size;
                    return result;
                }
            }
            catch (EndOfStreamException ex)
            {
                Debug.WriteLine(ex);
            }

            return new ReplayKey(TimeSpan.Zero, (Data.ReplayKeys) (-1), null);
        }

        private ReplayKey DecodeSubNext(BinaryReader reader, int remainingLength, ReplayKey originalKey)
        {
            try
            {
                var key = (Data.ReplayKeys) reader.ReadInt32();
                var position = reader.BaseStream.Position;
                var totalSize = position + remainingLength;

                if (!Enum.IsDefined(key))
                {
                    Debug.WriteLine($"Key doesn't exist {(int) key} @ length {reader.BaseStream.Position}");
                    var result =
                        new ReplayKey(originalKey.TimeStamp, key,
                            HandleUnhandled(reader,
                                (int)(remainingLength-(reader.BaseStream.Position-position)))); //new Tuple<Data.ReplayKeys, object>(key,null);
                    return result;
                }
                else
                {
                    Debug.WriteLine($"Key found {(int) key} @ length {reader.BaseStream.Position}");
                    var result = new ReplayKey(originalKey.TimeStamp,
                        key, Decode(key, reader));
                    var possibleLength = reader.BaseStream.Position - (position);
                    if (reader.BaseStream.Position > totalSize)
                    {
                        Debug.WriteLine("Exceeded length by " + (possibleLength) + "bytes");
                    }
                    else if (reader.BaseStream.Position < totalSize)
                    {
                        remainingLength =(int) (remainingLength-(reader.BaseStream.Position-position));
                        Debug.WriteLine("lost length by " + (possibleLength) + "bytes");
                        result.SubKey.Add(DecodeSubNext(reader, (int)possibleLength, originalKey));
                        //Debug.WriteLine(BitConverter.ToString(reader.ReadBytes((int) possibleLength)));
                    }

                    return result;
                }
            }
            catch (EndOfStreamException ex)
            {
                Debug.WriteLine(ex);
            }

            return new ReplayKey(TimeSpan.Zero, (Data.ReplayKeys) (-1), null);
        }

        private Unhandled HandleUnhandled(BinaryReader reader, int size)
        {
            return new()
            {
                Unknown = reader.ReadBytes(size)
            };
        }

        public List<ReplayKey> DecodeFile(BinaryReader reader)
        {
            var results = new List<ReplayKey>();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var result = DecodeNext(reader);
                results.Add(result);
            }

            return results;
        }
    }
}