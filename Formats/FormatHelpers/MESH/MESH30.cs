using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.MESH
{
    public class MESH30 : MESH2F
    {
        public MESH30(byte[] fileData, int iPos)
            : base(fileData, iPos)
        {
        }

        public override int Read(ref int referencecounter)
        {
            iPos += 4;
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Parts: 0x{1:x8}", iPos, num);
            iPos += 4;
            for (int i = 0; i < num; i++)
            {
                ColoredConsole.WriteLine("{0:x8}   Part 0x{1:x8}", iPos, i);
                Parts.Add(ReadPart(ref referencecounter));
            }
            return iPos;
        }
    }
}