namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH04 : TXGH03
    {
        public TXGH04(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        protected override void ReadTextureMeta()
        {
            iPos += 16;
            iPos += 4;
            iPos += 4;
            iPos += 29;
        }
    }
}