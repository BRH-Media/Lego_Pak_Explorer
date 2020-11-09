using System;
using System.Collections.Generic;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
    public class DISP17 : DISP15
    {
        public DISP17(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            readString(int32_1);
            iPos += 4;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            var intList1 = new List<int>();
            for (var index = 0; index < int32_2; ++index)
            {
                var int16 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                intList1.Add(int32_3);
                if (int16 == (short)-19712)
                    ColoredConsole.WriteLineDebug("{0} --> {1}", (object)index, (object)int32_3);
            }
            iPos += 4;
            var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            var intListList = new List<List<int>>();
            for (var index1 = 0; index1 < int32_4; ++index1)
            {
                var intList2 = new List<int>();
                BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                for (var index2 = 0; index2 < int32_3; ++index2)
                {
                    BigEndianBitConverter.ToInt32(fileData, iPos);
                    iPos += 4;
                }
                var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                for (var index2 = 0; index2 < int32_5; ++index2)
                {
                    var int32_6 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    iPos += 4;
                    intList2.Add(intList1[int32_6]);
                }
                intListList.Add(intList2);
            }
            iPos += 4;
            var int32_7 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += 2 * int32_7;
            iPos += 4;
            var int32_8 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += 4 * int32_8;
            iPos += 4;
            var int32_9 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_9; ++index)
            {
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                readStringAt(int32_3 + 1443);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 4), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 8), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 16), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 20), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 24), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 32), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 36), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 40), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 48) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 52) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 56) * 262.0, 4);
                iPos += 64;
                iPos += 16;
                iPos += 4;
                iPos += 16;
                iPos += 4;
                iPos += 80;
                var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4 * int32_5;
                BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4;
                iPos += 4;
            }
            iPos += 4;
            iPos += 5;
            iPos += 4;
            var int32_10 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_10; ++index)
                iPos += 16;
            iPos += 4;
            var int32_11 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_11; ++index)
                iPos += 16;
            iPos += 4;
            var int32_12 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_12; ++index)
                iPos += 44;
            iPos += 4;
            ColoredConsole.WriteLineError("{0:x8}", (object)iPos);
            return iPos;
        }

        private new string readString(int numberofchars)
        {
            var stringBuilder = new StringBuilder();
            for (var index = 0; index < numberofchars; ++index)
            {
                if (fileData[iPos] != (byte)0)
                    stringBuilder.Append((char)fileData[iPos]);
                ++iPos;
            }
            return stringBuilder.ToString();
        }

        private string readStringAt(int pos)
        {
            var stringBuilder = new StringBuilder();
            for (; fileData[pos] != (byte)0; ++pos)
                stringBuilder.Append((char)fileData[pos]);
            return stringBuilder.ToString();
        }
    }
}