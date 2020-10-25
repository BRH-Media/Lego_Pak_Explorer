namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH03 : TXGH01
    {
        public TXGH03(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        protected override void ReadTextureMeta()
        {
            this.iPos += 16;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 46;
        }
    }
}