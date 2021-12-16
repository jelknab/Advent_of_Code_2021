using Advent_of_Code_2021;
using Advent_of_Code_2021.Day_16;
using Xunit;

namespace Tests
{
    public class Day16Tests
    {
        [Fact]
        public void TestInputParser()
        {
            var hexToBits = Day16.ParseInputToBitString("D2FE28");
            Assert.Equal("110100101111111000101000", hexToBits);
            Assert.Equal(6, Day16.ReadValue(hexToBits, 0, 3));

            var packet = new LiteralPacket()
            {
                Position = 0
            };
            packet.ParsePacket(hexToBits);
            
            Assert.Equal(2021, packet.Value);
        }

        [Fact]
        public void TestOperatorPackageParser()
        {
            var hexToBits = Day16.ParseInputToBitString("38006F45291200");
            var packet = (OperatorPacket) Day16.ParsePacket(hexToBits, 0);
            packet.ParsePacket(hexToBits);
            
            Assert.Equal(1, packet.Version);
            Assert.Equal(6, packet.TypeId);
            Assert.Equal(10, ((LiteralPacket) packet.SubPackets[0]).Value);
            Assert.Equal(20, ((LiteralPacket) packet.SubPackets[1]).Value);
        }
        
        [Fact]
        public void TestOperatorPackageParser2()
        {
            var hexToBits = Day16.ParseInputToBitString("EE00D40C823060");
            var packet = (OperatorPacket) Day16.ParsePacket(hexToBits, 0);
            packet.ParsePacket(hexToBits);
            
            Assert.Equal(7, packet.Version);
            Assert.Equal(3, packet.TypeId);
            Assert.Equal(1, ((LiteralPacket) packet.SubPackets[0]).Value);
            Assert.Equal(2, ((LiteralPacket) packet.SubPackets[1]).Value);
            Assert.Equal(3, ((LiteralPacket) packet.SubPackets[2]).Value);
        }

        private int GetVersionSumForInput(string input)
        {
            var hexToBits = Day16.ParseInputToBitString(input);
            var packet = (OperatorPacket) Day16.ParsePacket(hexToBits, 0);
            return packet.SumVersions();
        }

        private long GetValueForInput(string input)
        {
            var hexToBits = Day16.ParseInputToBitString(input);
            var packet = (OperatorPacket) Day16.ParsePacket(hexToBits, 0);
            return packet.GetValue();
        }
        
        [Fact]
        public void TestAgainstExample() {
            Assert.Equal(16, GetVersionSumForInput("8A004A801A8002F478"));
            Assert.Equal(12, GetVersionSumForInput("620080001611562C8802118E34"));
            Assert.Equal(23, GetVersionSumForInput("C0015000016115A2E0802F182340"));
            Assert.Equal(31, GetVersionSumForInput("A0016C880162017C3686B18A3D4780"));
            
            Assert.Equal(3, GetValueForInput("C200B40A82"));
            Assert.Equal(54, GetValueForInput("04005AC33890"));
            Assert.Equal(7, GetValueForInput("880086C3E88112"));
            Assert.Equal(9, GetValueForInput("CE00C43D881120"));
            Assert.Equal(1, GetValueForInput("D8005AC2A8F0"));
            Assert.Equal(0, GetValueForInput("F600BC2D8F"));
            Assert.Equal(0, GetValueForInput("9C005AC2F8F0"));
            Assert.Equal(1, GetValueForInput("9C0141080250320F1802104A08"));
        }
    }
}