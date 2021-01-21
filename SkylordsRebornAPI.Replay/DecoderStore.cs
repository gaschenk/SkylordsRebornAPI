﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

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
                var inst = ctor.Invoke(new object[] {data});
                return inst;
            }

            return null;
        }

        public Tuple<Data.ReplayKeys, object> DecodeNext(BinaryReader reader)
        {
            try
            {
                var key = (Data.ReplayKeys) reader.ReadInt32();

                if (!Enum.IsDefined(key))
                {
                    Console.WriteLine($"Key doesn't exist {(int) key}");
                    return null;
                }
                else
                {
                    Console.WriteLine($"Key found {(int) key}");
                    return new Tuple<Data.ReplayKeys, object>(key, Decode(key, reader));
                }
                    
            }
            catch (EndOfStreamException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public List<Tuple<Data.ReplayKeys, object>> DecodeFile(BinaryReader reader)
        {
            var results = new List<Tuple<Data.ReplayKeys, object>>();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                Console.WriteLine("Current Position "+reader.BaseStream.Position+"/"+reader.BaseStream.Length);
                var result = DecodeNext(reader);
                if (result != null)
                    results.Add(result);
            }

            return results;
        }
    }
}