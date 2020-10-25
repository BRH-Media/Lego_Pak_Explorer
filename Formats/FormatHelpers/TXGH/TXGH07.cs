using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH07 : TXGH06
    {
        public TXGH07(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
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
            int iPos = this.iPos;
            string str = this.readString(int32);
            ColoredConsole.WriteLineInfo("{0:x8}     {2:0000} {1}", (object)iPos, (object)str, (object)this.Names.Count);
            this.Names.Add(str);
            this.iPos += 10;
        }
    }
}