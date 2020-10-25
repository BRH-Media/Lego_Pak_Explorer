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
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.readString(int32_1);
            this.iPos += 4;
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            List<int> intList1 = new List<int>();
            for (int index = 0; index < int32_2; ++index)
            {
                short int16 = BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                intList1.Add(int32_3);
                if (int16 == (short)-19712)
                    ColoredConsole.WriteLineDebug("{0} --> {1}", (object)index, (object)int32_3);
            }
            this.iPos += 4;
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            List<List<int>> intListList = new List<List<int>>();
            for (int index1 = 0; index1 < int32_4; ++index1)
            {
                List<int> intList2 = new List<int>();
                BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                for (int index2 = 0; index2 < int32_3; ++index2)
                {
                    BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                    this.iPos += 4;
                }
                int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                for (int index2 = 0; index2 < int32_5; ++index2)
                {
                    int int32_6 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                    this.iPos += 4;
                    intList2.Add(intList1[int32_6]);
                }
                intListList.Add(intList2);
            }
            this.iPos += 4;
            int int32_7 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += 2 * int32_7;
            this.iPos += 4;
            int int32_8 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += 4 * int32_8;
            this.iPos += 4;
            int int32_9 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_9; ++index)
            {
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.readStringAt(int32_3 + 1443);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 4), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 8), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 16), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 20), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 24), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 32), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 36), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 40), 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 48) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 52) * 262.0, 4);
                Math.Round((double)BigEndianBitConverter.ToSingle(this.fileData, this.iPos + 56) * 262.0, 4);
                this.iPos += 64;
                this.iPos += 16;
                this.iPos += 4;
                this.iPos += 16;
                this.iPos += 4;
                this.iPos += 80;
                int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += 4 * int32_5;
                BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
            }
            this.iPos += 4;
            this.iPos += 5;
            this.iPos += 4;
            int int32_10 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_10; ++index)
                this.iPos += 16;
            this.iPos += 4;
            int int32_11 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_11; ++index)
                this.iPos += 16;
            this.iPos += 4;
            int int32_12 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_12; ++index)
                this.iPos += 44;
            this.iPos += 4;
            ColoredConsole.WriteLineError("{0:x8}", (object)this.iPos);
            return this.iPos;
        }

        private new string readString(int numberofchars)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < numberofchars; ++index)
            {
                if (this.fileData[this.iPos] != (byte)0)
                    stringBuilder.Append((char)this.fileData[this.iPos]);
                ++this.iPos;
            }
            return stringBuilder.ToString();
        }

        private string readStringAt(int pos)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (; this.fileData[pos] != (byte)0; ++pos)
                stringBuilder.Append((char)this.fileData[pos]);
            return stringBuilder.ToString();
        }
    }
}