using System;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH0A : TXGH08
    {
        public TXGH0A(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(ref int referencecounter)
        {
            iPos += 4;
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_1; ++index)
            {
                if (BigEndianBitConverter.ToInt32(fileData, iPos) != 0)
                    throw new NotSupportedException("TXGH VTOR 1");
                iPos += 4;
            }
            iPos += 4;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)iPos, (object)int32_2);
            for (var index = 0; index < int32_2; ++index)
            {
                iPos += 16;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                Names.Add(readString(int32_3));
                iPos += 4;
                if (int32_3 != 0)
                    ++referencecounter;
            }
            iPos += 4;
            referencecounter += BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            return iPos;
        }
    }
}