using System.Collections.Generic;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
    public class DISP15
    {
        protected byte[] fileData;

        protected int iPos;

        public DISP15(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read()
        {
            int numberofchars = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            string text = readString(numberofchars);
            ColoredConsole.WriteLine("{0:x8}       Name: {1}", iPos, text);
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
                int num5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4 * num5;
                int num6 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4 * num6;
            }
            iPos += 4;
            int num7 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += 2 * num7;
            iPos += 4;
            int num8 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += 4 * num8;
            iPos += 4;
            int num9 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num9; i++)
            {
                iPos += 4;
                iPos += 64;
                iPos += 16;
                iPos += 4;
                iPos += 16;
                iPos += 4;
                iPos += 80;
                int num10 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 4 * num10;
                iPos += 12;
            }
            iPos += 4;
            iPos += 5;
            iPos += 4;
            int num11 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num11; i++)
            {
                iPos += 16;
            }
            iPos += 4;
            int num12 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num12; i++)
            {
                iPos += 16;
            }
            iPos += 4;
            int num13 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (int i = 0; i < num13; i++)
            {
                iPos += 44;
            }
            iPos += 4;
            ColoredConsole.WriteLineError("{0:x8}", iPos);
            return iPos;
        }

        private string readString(int numberofchars)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < numberofchars; i++)
            {
                if (fileData[iPos] != 0)
                {
                    stringBuilder.Append((char)fileData[iPos]);
                }
                iPos++;
            }
            return stringBuilder.ToString();
        }
    }
}