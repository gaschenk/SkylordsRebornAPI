using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SkylordsRebornAPI.Replay.Data;

namespace SkylordsRebornAPI.Replay
{
    public class ReplayReader
    {
        private List<byte> _bytes = new();

        public Data.Replay ReadReplay(string path)
        {
            var bytes = new List<byte>();
            using (var reader = new BinaryReader(File.OpenRead(path)))
            {
                var sanityCheck = reader.ReadBytes(3);

                if (Encoding.UTF8.GetString(sanityCheck) == "PMV")
                {
                    //var replay = ReadMetaInformation(reader);
                    var replay = ReadInformation(reader);
                    return replay;
                }

                throw new Exception();
            }
        }


        public Data.Replay ReadInformation(BinaryReader reader)
        {
            var replay = new Data.Replay();

            replay.ReplayRevision = reader.ReadUInt32();

            replay.GameVersion = 0;

            if (replay.ReplayRevision > 200)
                replay.GameVersion = reader.ReadUInt32();

            replay.PlayTime = TimeSpan.FromMilliseconds(reader.ReadUInt32() * 100);

            // "header_new_dummy" dropped
            if (replay.ReplayRevision > 200)
                reader.ReadBytes(4);

            replay.MapPath = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadInt32()));

            var headerSizeUntilActions = reader.ReadUInt32() + reader.BaseStream.Position;

            // Dumping: v_7 players_per_team v_0200
            reader.ReadBytes(2 + 4 + 1 + 2);

            replay.HostPlayerId = reader.ReadUInt64();

            var groupCount = reader.ReadByte();

            var matrixLength = reader.ReadUInt16();

            replay.Matrix = new();

            // "contains who is allied to which player"?
            for (int i = 0; i < matrixLength; i++)
            {
                replay.Matrix.Add(new MatrixEntry
                {
                    X = reader.ReadByte(),
                    Y = reader.ReadByte(),
                    Z = reader.ReadByte(),
                });
            }

            replay.Teams = new();

            var amountOfTeams = reader.ReadUInt16();
            
            Console.WriteLine(amountOfTeams);

            for (int i = 0; i < amountOfTeams; i++)
            {
                var length = reader.ReadInt32();
                replay.Teams.Add(new Team()
                {
                    Name = Encoding.ASCII.GetString(reader.ReadBytes(length)),
                    TeamId = reader.ReadInt32(),
                    Players = new List<Player>(),
                    // NOT AN NPC FLAG FFS
                    Unknown = reader.ReadBytes(2)
                });

            }

            while (reader.BaseStream.Position < headerSizeUntilActions)
            {
                var player = ReadPlayer(reader, out byte teamId);
                
                replay.Teams.First(team => team.TeamId == teamId).Players.Add(player);
            }

            replay.ReplayKeys = ReadActions(reader);
            
            return replay;
        }


        private List<Tuple<Data.ReplayKeys, object>> ReadActions(BinaryReader reader)
        {
            var replayKeys = new List<Tuple<Data.ReplayKeys, object>>();
            try
            {
                // The fuck time?
                //
                //var data = reader.ReadBytes((int) size)
                var decoderStore = new DecoderStore();
                replayKeys = decoderStore.DecodeFile(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return replayKeys;
        }

        private Card ReadCard(BinaryReader reader)
        {
            return new()
            {
                Id = reader.ReadUInt16(),
                //Unsure?
                Upgrades = reader.ReadUInt16(),
                //Unsure?
                Charges = reader.ReadByte()
            };
        }

        private Player ReadPlayer(BinaryReader reader, out byte groupId)
        {
            var name = ReadName(reader, reader.ReadInt32());
            Console.WriteLine($"name:{name}");

            var playerId = reader.ReadUInt64();
            Console.WriteLine("PlayerID:" + playerId);

            groupId = reader.ReadByte();
            Console.WriteLine("groupId:" + groupId);

            // Useless?
            var subgroupId = reader.ReadByte();
            Console.WriteLine("subgroupId:" + subgroupId);

            var deckType = reader.ReadByte();
            Console.WriteLine("deckType:" + deckType);

            var cardCount = reader.ReadByte();
            Console.WriteLine("cardCount:" + cardCount);

            //Whatever this is
            var anotherCardCount = reader.ReadByte();
            Console.WriteLine("anotherCardCount:" + anotherCardCount);

            var cards = new List<Card>();
            for (var i = 0; i < cardCount; i++)
                cards.Add(ReadCard(reader));

            return new Player
            {
                Cards = cards,
                PlayerId = playerId,
                TeamSlot = subgroupId,
                Name = name
            };
        }

        private Team ReadTeam(BinaryReader reader)
        {
            var length = reader.ReadInt32();
            Console.WriteLine("teamNamelength: "+length);

            var teamName = Encoding.ASCII.GetString(reader.ReadBytes(length));
            Console.WriteLine(teamName);

            var teamId = reader.ReadInt32();
            reader.ReadBytes(2);

            return new Team
            {
                Name = teamName,
                TeamId = teamId,
                Players = new List<Player>()
            };
        }

        private string ReadName(BinaryReader reader, int length)
        {
            return Encoding.Unicode.GetString(reader.ReadBytes(length * 2));
        }
    }
}