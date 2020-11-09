using System;
using System.Collections.Generic;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
    public class DISP21 : DISP17
    {
        public DISP21(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            iPos += 4;
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            var intList = new List<int>();
            for (var index = 0; index < int32_1; ++index)
            {
                var int16 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                if (int16 == (short)-19712)
                    intList.Add(int32_2);
            }
            iPos += 4;
            var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_3; ++index)
            {
                var int16 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                iPos += 8 * (int)int16;
            }
            iPos += 4;
            var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_4; ++index)
            {
                var int16 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                iPos += (int)int16;
                iPos += 64;
                iPos += 56;
                iPos += 20;
            }
            iPos += 4;
            iPos += 4;
            iPos += 4;
            var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_5; ++index)
                iPos += 16;
            iPos += 4;
            var int32_6 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_6; ++index)
                iPos += 16;
            iPos += 4;
            var int32_7 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_7; ++index)
                iPos += 64;
            iPos += 4;
            iPos += 4;
            iPos += 4;
            var int32_8 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_8; ++index)
                iPos += 4;
            iPos += 4;
            var int32_9 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_9; ++index)
            {
                var num1 = Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 4), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 8), 4);
                var num2 = Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 12), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 16), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 20), 4);
                var num3 = Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 24), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 28), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 32), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 36) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 40) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(fileData, iPos + 44) * 262.0, 4);
                var num4 = 16;
                if (num1 == 1.0 && num2 == 0.0 && num3 == 0.0)
                    num4 = 1;
                if (num1 == -1.0 && num2 == 0.0 && num3 == 0.0)
                    num4 = 2;
                if (num1 == 0.0 && num2 == 0.0 && num3 == 1.0)
                    num4 = 3;
                if (num1 == 0.0 && num2 == 0.0 && num3 == -1.0)
                    num4 = 5;
                iPos += 48;
            }
            ColoredConsole.WriteLineError("{0:x8}", (object)iPos);
            return iPos;
        }
    }
}