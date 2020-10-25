using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.UMTL
{
    public class UMTL34 : UMTL00
    {
        public UMTL34(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            for (int index = 0; index < int32_1; ++index)
            {
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 305;
                int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += 20;
                int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += 469;
                int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                string name = this.readString(int32_4);
                this.iPos += 124;
                int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += int32_5 * 3;
                int int32_6 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                this.iPos += int32_6 * 3;
                this.iPos += 98;
                ColoredConsole.WriteLineInfo("{0:x8}   {4:0000} {1} --> Tex: {2}; Norm: {3}", (object)this.iPos, (object)name, (object)int32_2, (object)int32_3, (object)index);
                this.Materials.Add(new Material(name, int32_2, int32_3));
            }
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