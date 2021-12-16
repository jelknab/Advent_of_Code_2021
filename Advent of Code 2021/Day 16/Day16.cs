using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_16
{
    public abstract class Packet
    {
        public int Version { get; set; }

        public int TypeId { get; set; }

        public int PacketSize { get; set; }

        public int Position { get; set; }

        public abstract void ParsePacket(string bitString);

        public abstract long GetValue();

        public abstract int SumVersions();
    }

    public class LiteralPacket: Packet
    {
        public long Value { get; set; }

        public override void ParsePacket(string bitString)
        {
            PacketSize = 6;
            var valueBits = "";
            for (var i = 0;; i++)
            {
                valueBits += bitString.Substring(Position + 7 + i * 5, 4);
                PacketSize += 5;

                if (Day16.ReadValue(bitString, Position + 6 + i * 5, 1) == 0)
                {
                    break;
                }
            }

            Value = Convert.ToInt64(valueBits, 2);
        }

        public override long GetValue()
        {
            return Value;
        }

        public override int SumVersions()
        {
            return Version;
        }
    }

    public class OperatorPacket : Packet
    {
        public List<Packet> SubPackets { get; set; } = new List<Packet>();

        public override void ParsePacket(string bitString)
        {
            var lengthTypeID = Day16.ReadValue(bitString, Position + 6, 1);

            if (lengthTypeID == 0)
            {
                var subPacketBits = Day16.ReadValue(bitString, Position + 7, 15);
                PacketSize = 22 + subPacketBits;
                ParseSubPackets(bitString.Substring(Position + 22, subPacketBits));
            }

            if (lengthTypeID == 1)
            {
                var subPacketCount = Day16.ReadValue(bitString, Position + 7, 11);
                ParseSubPackets(bitString, Position + 18, subPacketCount);
                PacketSize = 18 + SubPackets.Sum(packet => packet.PacketSize);
            }
        }

        public override long GetValue()
        {
            return TypeId switch
            {
                0 => SubPackets.Select(sub => sub.GetValue()).Sum(),
                1 => SubPackets.Select(sub => sub.GetValue()).Aggregate((a, b) => a * b),
                2 => SubPackets.Select(sub => sub.GetValue()).Min(),
                3 => SubPackets.Select(sub => sub.GetValue()).Max(),
                5 => SubPackets[0].GetValue() > SubPackets[1].GetValue() ? 1L : 0L,
                6 => SubPackets[0].GetValue() < SubPackets[1].GetValue() ? 1L : 0L,
                7 => SubPackets[0].GetValue() == SubPackets[1].GetValue() ? 1L : 0L,
                _ => throw new Exception("Unknown type id")
            };
        }

        public override int SumVersions()
        {
            return Version + SubPackets.Sum(sub => sub.SumVersions());
        }

        private void ParseSubPackets(string bitString)
        {
            var cursor = 0;

            while (cursor < bitString.Length)
            {
                var subPacket = Day16.ParsePacket(bitString, cursor);
                SubPackets.Add(subPacket);
                cursor += subPacket.PacketSize;
            }
        }

        private void ParseSubPackets(string bitString, int positionStart, int amount)
        {
            var cursor = positionStart;

            for (var packetIndex = 0; packetIndex < amount; packetIndex++)
            {
                var subPacket = Day16.ParsePacket(bitString, cursor);
                SubPackets.Add(subPacket);
                cursor += subPacket.PacketSize;
            }
        }
    }

    public class Day16 : IDay
    {
        public const int LiteralPacketId = 4;

        public static string ParseInputToBitString(string hexInput)
        {
            return string.Join(string.Empty,
                hexInput.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
        }

        public static Packet ParsePacket(string bitString, int position)
        {
            if (ReadValue(bitString, position + 3, 3) == LiteralPacketId)
            {
                var literalPackage = new LiteralPacket()
                {
                    Position = position,
                    Version = ReadValue(bitString, position, 3),
                    TypeId = ReadValue(bitString, position + 3, 3),
                };
                literalPackage.ParsePacket(bitString);
                return literalPackage;
            }

            var operatorPackage = new OperatorPacket()
            {
                Position = position,
                Version = ReadValue(bitString, position, 3),
                TypeId = ReadValue(bitString, position + 3, 3),
            };
            operatorPackage.ParsePacket(bitString);
            return operatorPackage;
        }

        public static int ReadValue(string bits, int position, int length)
        {
            return Convert.ToInt32(bits.Substring(position, length), 2);
        }

        public void PrintSolution1()
        {
            var input = File.ReadAllText("Day 16/input").TrimEnd();
            var hexToBits = ParseInputToBitString(input);
            var packet = (OperatorPacket) ParsePacket(hexToBits, 0);
            Console.WriteLine(packet.SumVersions());
        }

        public void PrintSolution2()
        {
            var input = File.ReadAllText("Day 16/input").TrimEnd();
            var hexToBits = ParseInputToBitString(input);
            var packet = (OperatorPacket) ParsePacket(hexToBits, 0);
            Console.WriteLine(packet.GetValue());
        }
    }
}