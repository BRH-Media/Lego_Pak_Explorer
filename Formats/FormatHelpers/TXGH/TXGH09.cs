using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH09 : TXGH08
    {
        public TXGH09(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        protected override void ReadTextureMeta()
        {
            iPos += 16;
            iPos += 4;
            iPos += 4;
            iPos += 4;
            iPos += 4;
            iPos += 4;
            iPos += 17;
            var int32 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += int32;
            iPos += 14;
        }

        public override int Read(ref int referencecounter)
        {
            iPos += 4;
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)iPos, (object)int32_1);
            iPos += 4 * int32_1;
            iPos += 4;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)iPos, (object)int32_2);
            for (var index = 0; index < int32_2; ++index)
            {
                ReadTextureMeta();
                ++referencecounter;
            }
            iPos += 4;
            var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)iPos, (object)int32_3);
            iPos += 4 * int32_3;
            iPos += 4;
            var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Cameras: 0x{1:x2}", (object)iPos, (object)int32_4);
            for (var index = 0; index < int32_4; ++index)
                ReadCam();
            iPos += 4;
            var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)iPos, (object)int32_5);
            iPos += 2 * int32_5;
            return iPos;
        }
    }
}