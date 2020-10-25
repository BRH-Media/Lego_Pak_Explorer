using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH08 : TXGH07
    {
        public TXGH08(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(ref int referencecounter)
        {
            this.iPos += 4;
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_1);
            this.iPos += 4;
            this.iPos += 4 * int32_1;
            this.iPos += 4;
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)this.iPos, (object)int32_2);
            this.iPos += 4;
            for (int index = 0; index < int32_2; ++index)
            {
                this.ReadTextureMeta();
                ++referencecounter;
            }
            this.iPos += 4;
            int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_3);
            this.iPos += 4;
            this.iPos += 4 * int32_3;
            if (this.fileData[this.iPos] == (byte)82)
                this.iPos += 4;
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Cameras: 0x{1:x2}", (object)this.iPos, (object)int32_4);
            this.iPos += 4;
            for (int index = 0; index < int32_4; ++index)
                this.ReadCam();
            if (this.fileData[this.iPos] == (byte)82)
                this.iPos += 4;
            int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_5);
            if (int32_5 != 0)
                ++referencecounter;
            this.iPos += 2 * int32_5;
            return this.iPos;
        }
    }
}