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
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                    }

                    return replay;
                }

                throw new Exception();
            }
        }

        public Data.Replay ReadMetaInformation(BinaryReader reader)
        {
            var replay = new Data.Replay();
            replay.ReplayFormatVersion = reader.ReadUInt32();
            Console.WriteLine(replay.ReplayFormatVersion);

            if (replay.ReplayFormatVersion > 200)
                // V_gameversion_B7074000:0x407b7
                reader.ReadBytes(4);

            var timeValue = reader.ReadInt32();
            replay.PlayTime = TimeSpan.FromMilliseconds(timeValue * 100);
            Console.WriteLine(replay.PlayTime);
            if (replay.ReplayFormatVersion >= 218)
                reader.ReadBytes(4);

            replay.MapPath = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadInt32()));
            Console.WriteLine("path:" + replay.MapPath);

            //Somewhat useless
            var headerLengthUntilActions = reader.ReadUInt32();

            reader.ReadBytes(6);

            //Useless outside of pvp? well even then rather useless.
            var playersPerTeam = reader.ReadByte();
            Console.WriteLine("playersPerTeam" + playersPerTeam);

            // some other stupid value
            reader.ReadBytes(replay.ReplayFormatVersion > 200 ? 2 : 4);

            replay.HostPlayerId = reader.ReadUInt64();
            Console.WriteLine(replay.HostPlayerId);

            //group count??????????????????
            reader.ReadByte();

            /*var matrixLength = reader.ReadInt16();
            Console.WriteLine(matrixLength);
            replay.Matrix = new MatrixEntry[matrixLength/3];
            for (int i = 0; i < matrixLength/3; i++)
            {
                replay.Matrix[i] = new MatrixEntry(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            }
            
            //Dumped
            reader.ReadBytes(77-matrixLength);*/
            reader.ReadBytes(77);

            var amountOfTeams = reader.ReadInt16();
            Console.WriteLine("amountOfTeams:" + amountOfTeams);

            replay.Teams = new List<Team>();

            for (var i = 0; i < amountOfTeams; i++)
            {
                replay.Teams.Add(ReadTeam(reader));
            }

            var player = ReadPlayer(reader, out byte groupId);

            replay.Teams.First(team => team.TeamId == groupId).Players.Add(player);
            
            return replay;
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
            Console.WriteLine("cardcount:" + cardCount);
            
            //Whatever this is
            var anotherCardCount = reader.ReadByte();
            Console.WriteLine("anotherCardCount:" + anotherCardCount);
            
            var cards = new List<Card>();
            for (var i = 0; i < cardCount; i++)
                cards.Add(ReadCard(reader));
            
            return new Player()
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

            return new Team()
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