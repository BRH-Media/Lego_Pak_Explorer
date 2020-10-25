using System.Collections.Generic;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
    public class DISP0F
    {
        protected byte[] fileData;
        protected int iPos;
        public List<Group> Groups = new List<Group>();

        public DISP0F(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read()
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
                BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                intList.Add(int32_3);
            }
            this.iPos += 4;
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index1 = 0; index1 < int32_4; ++index1)
            {
                Group group = new Group();
                BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                for (int index2 = 0; index2 < int32_3; ++index2)
                {
                    int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                    ColoredConsole.WriteWarn("{0}; ", (object)int32_5);
                    this.iPos += 4;
                    group.Material.Add(int32_5);
                }
                ColoredConsole.WriteLine();
                int int32_6 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                for (int index2 = 0; index2 < int32_6; ++index2)
                {
                    int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                    ColoredConsole.WriteDebug("{0} --> {1}; ", (object)int32_5, (object)intList[int32_5]);
                    this.iPos += 4;
                    group.Parts.Add(intList[int32_5]);
                }
                ColoredConsole.WriteLine();
                this.Groups.Add(group);
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
            this.iPos += 4 * int32_9;
            int int32_10 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += 4 * int32_10;
            int int32_11 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += 4 * int32_11;
            this.iPos += 4;
            ColoredConsole.WriteLineError("{0:x8}", (object)this.iPos);
            return this.iPos;
        }

        protected string readString(int numberofchars)
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
    }
}