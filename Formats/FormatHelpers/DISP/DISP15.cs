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
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}       Name: {1}", (object)this.iPos, (object)this.readString(int32_1));
            this.iPos += 4;
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            List<int> intList = new List<int>();
            for (int index = 0; index < int32_2; ++index)
            {
                short int16 = BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                if (int16 == (short)-19712)
                    intList.Add(int32_3);
            }
            this.iPos += 4;
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_4; ++index)
            {
                BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += 4 * int32_3;
                int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += 4 * int32_5;
            }
            this.iPos += 4;
            int int32_6 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += 2 * int32_6;
            this.iPos += 4;
            int int32_7 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += 4 * int32_7;
            this.iPos += 4;
            int int32_8 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_8; ++index)
            {
                this.iPos += 4;
                this.iPos += 64;
                this.iPos += 16;
                this.iPos += 4;
                this.iPos += 16;
                this.iPos += 4;
                this.iPos += 80;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += 4 * int32_3;
                this.iPos += 12;
            }
            this.iPos += 4;
            this.iPos += 5;
            this.iPos += 4;
            int int32_9 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_9; ++index)
                this.iPos += 16;
            this.iPos += 4;
            int int32_10 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_10; ++index)
                this.iPos += 16;
            this.iPos += 4;
            int int32_11 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_11; ++index)
                this.iPos += 44;
            this.iPos += 4;
            ColoredConsole.WriteLineError("{0:x8}", (object)this.iPos);
            return this.iPos;
        }
    }
}