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
            this.iPos += 4;
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            List<int> intList = new List<int>();
            for (int index = 0; index < int32_1; ++index)
            {
                short int16 = BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                if (int16 == (short)-19712)
                    intList.Add(int32_2);
            }
            this.iPos += 4;
            int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_3; ++index)
            {
                short int16 = BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                this.iPos += 8 * (int)int16;
            }
            this.iPos += 4;
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_4; ++index)
            {
                short int16 = BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                this.iPos += (int)int16;
                this.iPos += 64;
                this.iPos += 56;
                this.iPos += 20;
            }
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_5; ++index)
                this.iPos += 16;
            this.iPos += 4;
            int int32_6 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_6; ++index)
                this.iPos += 16;
            this.iPos += 4;
            int int32_7 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_7; ++index)
                this.iPos += 64;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            int int32_8 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_8; ++index)
                this.iPos += 4;
            this.iPos += 4;
            int int32_9 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_9; ++index)
            {
                double num1 = Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 4), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 8), 4);
                double num2 = Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 12), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 16), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 20), 4);
                double num3 = Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 24), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 28), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 32), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 36) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 40) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 44) * 262.0, 4);
                int num4 = 16;
                if (num1 == 1.0 && num2 == 0.0 && num3 == 0.0)
                    num4 = 1;
                if (num1 == -1.0 && num2 == 0.0 && num3 == 0.0)
                    num4 = 2;
                if (num1 == 0.0 && num2 == 0.0 && num3 == 1.0)
                    num4 = 3;
                if (num1 == 0.0 && num2 == 0.0 && num3 == -1.0)
                    num4 = 5;
                this.iPos += 48;
            }
            ColoredConsole.WriteLineError("{0:x8}", (object)this.iPos);
            return this.iPos;
        }
    }
}