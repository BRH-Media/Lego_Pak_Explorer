using System.Collections.Generic;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
    public class DISP08 : DISP0F
    {
        public DISP08(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}       Name: {1}", (object)this.iPos, (object)this.readString(int32_1));
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
            ColoredConsole.WriteLineError("{0:x8}", (object)this.iPos);
            return this.iPos;
        }

        protected new string readString(int numberofchars)
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