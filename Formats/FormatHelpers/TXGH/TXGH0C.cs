using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH0C : TXGH0A
    {
        public TXGH0C(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(ref int referencecounter)
        {
            this.iPos += 4;
            int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)this.iPos, (object)int32);
            for (int index = 0; index < int32; ++index)
            {
                this.iPos += 16;
                this.iPos += 3;
                int int16 = (int)BigEndianBitConverter.ToInt16(this.fileData, this.iPos);
                this.iPos += 2;
                this.iPos += int16;
                ++this.iPos;
                if (int16 != 0)
                    ++referencecounter;
            }
            return this.iPos;
        }
    }
}