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
            iPos += 4;
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", iPos, num);
            for (int i = 0; i < num; i++)
            {
                iPos += 16;
                iPos += 3;
                int num2 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                iPos += num2;
                iPos++;
                if (num2 != 0)
                {
                    referencecounter++;
                }
            }
            return iPos;
        }
    }
}