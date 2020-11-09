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
            var int32 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)iPos, (object)int32);
            for (var index = 0; index < int32; ++index)
            {
                iPos += 16;
                iPos += 3;
                var int16 = (int)BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                iPos += int16;
                ++iPos;
                if (int16 != 0)
                    ++referencecounter;
            }
            return iPos;
        }
    }
}