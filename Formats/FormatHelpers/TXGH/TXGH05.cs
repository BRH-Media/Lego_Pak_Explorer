using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH05 : TXGH04
    {
        public TXGH05(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(ref int referencecounter)
        {
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_1);
            if (int32_1 != 0)
                ++referencecounter;
            this.iPos += 4 * int32_1;
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)this.iPos, (object)int32_2);
            if (int32_2 != 0)
                ++referencecounter;
            for (int index = 0; index < int32_2; ++index)
            {
                this.ReadTextureMeta();
                ++referencecounter;
            }
            int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_3);
            this.iPos += 4 * int32_3;
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Cameras: 0x{1:x2}", (object)this.iPos, (object)int32_4);
            if (int32_3 != 0)
                ++referencecounter;
            for (int index = 0; index < int32_4; ++index)
            {
                this.ReadCam();
                ++referencecounter;
            }
            int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_5);
            if (int32_5 != 0)
                ++referencecounter;
            this.iPos += 2 * int32_5;
            return this.iPos;
        }

        protected override void ReadTextureMeta()
        {
            this.iPos += 16;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 17;
            int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += int32;
        }

        protected override void ReadCam()
        {
            this.iPos += 4;
            int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            this.iPos += int32 * 12;
        }
    }
}