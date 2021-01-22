using System;
using System.Collections.Generic;
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
                    var replay = ReadMetaInformation(reader);

                    return replay;
                }

                throw new Exception();
            }
        }

        public Data.Replay ReadMetaInformation(BinaryReader reader)
        {
            var replay = new Data.Replay();
            replay.ReplayRevision = reader.ReadUInt32();
            Console.WriteLine(replay.ReplayRevision);

            if (!(replay.ReplayRevision <= 200))
                // V_gameversion_B7074000:0x407b7
                reader.ReadBytes(4);

            var timeValue = reader.ReadInt32();
            replay.PlayTime = TimeSpan.FromMilliseconds(timeValue * 100);
            Console.WriteLine(replay.PlayTime);
            if (!(replay.ReplayRevision < 218))
                reader.ReadBytes(4);

            replay.MapPath = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadInt32()));
            Console.WriteLine("path:" + replay.MapPath);

            //Somewhat useless 
            // It's not equal when the header ends?
            var headerLengthUntilActions = reader.ReadUInt32();
            Console.WriteLine("headerLengthUntilActions"+headerLengthUntilActions);

            reader.ReadBytes(6);

            //Useless outside of pvp? well even then rather useless.
            var playersPerTeam = reader.ReadByte();
            Console.WriteLine("playersPerTeam" + playersPerTeam);

            // some other stupid value
            reader.ReadBytes(replay.ReplayRevision <= 200 ? 4 : 2);

            replay.HostPlayerId = reader.ReadUInt64();
            Console.WriteLine("hostPlayerId"+replay.HostPlayerId);

            //group count??????????????????
            reader.ReadByte();

            var matrixLength = reader.ReadInt16();
            Console.WriteLine("matrixlength: "+matrixLength);
            replay.Matrix = new List<MatrixEntry>();
            var pos = 0;
            while(pos <matrixLength)
            {
                replay.Matrix.Add(new MatrixEntry()
                {
                    X = reader.ReadByte(),
                    Y = reader.ReadByte(),
                    Z = reader.ReadByte()
                });
                pos += 3;
            }
            var headerLength = reader.ReadUInt16()-1;
            replay.ShitHeaders = new List<ShitHeader>();
            Console.WriteLine("headerLength"+headerLength);
            /*
            pos = 0;
            while(pos <headerLength)
            {
                replay.ShitHeaders.Add(ReadShitHeader(reader, replay.ReplayRevision, out int length));
                pos += length;
            }*/
            Console.WriteLine("position"+pos+"/"+headerLength);
            //reader.ReadBytes(70);
            //reader.ReadBytes(70);

            var amountOfTeams = reader.ReadInt16();
            Console.WriteLine("amountOfTeams:" + amountOfTeams);

            replay.Teams = new List<Team>();

            for (var i = 0; i < amountOfTeams; i++) replay.Teams.Add(ReadTeam(reader));

            var player = ReadPlayer(reader, out var groupId);

            replay.Teams.First(team => team.TeamId == groupId).Players.Add(player);
            
            //reader.BaseStream.Position=headerLengthUntilActions;
            replay.ReplayKeys = ReadActions(reader);
            Console.WriteLine(replay.ReplayKeys.Count);

            return replay;
        }

        public ShitHeader ReadShitHeader(BinaryReader reader, uint revision, out int length)
        {
            var shitHeader = new ShitHeader();
            shitHeader.GroupId = reader.ReadUInt32();
            length = (revision <= 200 ? 3 : 2);
            reader.ReadBytes(length);
            length += 4;
            return shitHeader;
            
        }


        private List<Tuple<Data.ReplayKeys,object>> ReadActions(BinaryReader reader)
        {
            var replayKeys = new List<Tuple<Data.ReplayKeys,object>>();
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
                Name = name
            };
        }

        private Team ReadTeam(BinaryReader reader)
        {
            var length = reader.ReadInt32();
            Console.WriteLine(length);

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
    public struct ShitHeader
    {
        public uint GroupId { get; set; }

        public byte[] Unknown { get; set; }
    }
}