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
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            List<int> list = new List<int>();
            for (int i = 0; i < num; i++)
            {
                short num2 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                int item = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                if (num2 == -19712)
                {
                    list.Add(item);
                }
            }
            iPos += 4;
            int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num3; i++)
            {
                short num4 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                iPos += 8 * num4;
            }
            iPos += 4;
            int num5 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num5; i++)
            {
                short num6 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                iPos += num6;
                iPos += 64;
                iPos += 56;
                iPos += 20;
            }
            iPos += 4;
            iPos += 4;
            iPos += 4;
            int num7 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num7; i++)
            {
                iPos += 16;
            }
            iPos += 4;
            int num8 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num8; i++)
            {
                iPos += 16;
            }
            iPos += 4;
            int num9 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num9; i++)
            {
                iPos += 64;
            }
            iPos += 4;
            iPos += 4;
            iPos += 4;
            int num10 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num10; i++)
            {
                iPos += 4;
            }
            iPos += 4;
            int num11 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num11; i++)
            {
                double num12 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos), 4);
                double num13 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 4), 4);
                double num14 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 8), 4);
                double num15 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 12), 4);
                double num16 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 16), 4);
                double num17 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 20), 4);
                double num18 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 24), 4);
                double num19 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 28), 4);
                double num20 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 32), 4);
                double num21 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 36) * 262f, 4);
                double num22 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 40) * 262f, 4);
                double num23 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 44) * 262f, 4);
                int num24 = 16;
                if (num12 == 1.0 && num15 == 0.0 && num18 == 0.0)
                {
                    num24 = 1;
                }
                if (num12 == -1.0 && num15 == 0.0 && num18 == 0.0)
                {
                    num24 = 2;
                }
                if (num12 == 0.0 && num15 == 0.0 && num18 == 1.0)
                {
                    num24 = 3;
                }
                if (num12 == 0.0 && num15 == 0.0 && num18 == -1.0)
                {
                    num24 = 5;
                }
                iPos += 48;
            }
            ColoredConsole.WriteLineError("{0:x8}", iPos);
            return iPos;
        }
    }
}