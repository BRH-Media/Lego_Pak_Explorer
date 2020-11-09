using System.Collections.Generic;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
    public class DISP15 : DISP0F
    {
        public DISP15(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}       Name: {1}", (object)iPos, (object)readString(int32_1));
            iPos += 4;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            var intList = new List<int>();
            for (var index = 0; index < int32_2; ++index)
            {
                var int16 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                if (int16 == (short)-19712)
                    intList.Add(int32_3);
            }
            iPos += 4;
            var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_4; ++index)
            {
                BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4 * int32_3;
                var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4 * int32_5;
            }
            iPos += 4;
            var int32_6 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += 2 * int32_6;
            iPos += 4;
            var int32_7 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += 4 * int32_7;
            iPos += 4;
            var int32_8 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_8; ++index)
            {
                iPos += 4;
                iPos += 64;
                iPos += 16;
                iPos += 4;
                iPos += 16;
                iPos += 4;
                iPos += 80;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4 * int32_3;
                iPos += 12;
            }
            iPos += 4;
            iPos += 5;
            iPos += 4;
            var int32_9 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_9; ++index)
                iPos += 16;
            iPos += 4;
            var int32_10 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_10; ++index)
                iPos += 16;
            iPos += 4;
            var int32_11 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_11; ++index)
                iPos += 44;
            iPos += 4;
            ColoredConsole.WriteLineError("{0:x8}", (object)iPos);
            return iPos;
        }
    }
}