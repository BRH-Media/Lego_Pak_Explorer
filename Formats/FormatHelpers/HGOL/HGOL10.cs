using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.HGOL
{
    public class HGOL10 : HGOL01
    {
        public HGOL10(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            this.iPos += 4;
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", (object)this.iPos, (object)int32_1);
            this.iPos += 4;
            for (int index = 0; index < int32_1; ++index)
            {
                short int16 = BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                ColoredConsole.WriteLine("{0:x8}       Name: {1}", (object)this.iPos, (object)this.readString((int)int16));
                this.iPos += 64;
                this.iPos += 14;
            }
            this.iPos += 4;
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", (object)this.iPos, (object)int32_2);
            this.iPos += 4;
            for (int index = 0; index < int32_2; ++index)
                this.iPos += 64;
            this.iPos += 4;
            int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", (object)this.iPos, (object)int32_3);
            this.iPos += 4;
            for (int index = 0; index < int32_3; ++index)
                this.iPos += 64;
            return this.iPos;
        }

        private string readString(int numberofchars)
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