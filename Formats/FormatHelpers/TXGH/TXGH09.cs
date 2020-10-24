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
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            iPos += num;
            iPos += 14;
        }

        public override int Read(ref int referencecounter)
        {
            iPos += 4;
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", iPos, num);
            iPos += 4 * num;
            iPos += 4;
            int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", iPos, num2);
            for (int i = 0; i < num2; i++)
            {
                ReadTextureMeta();
                referencecounter++;
            }
            iPos += 4;
            num = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", iPos, num);
            iPos += 4 * num;
            iPos += 4;
            int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Cameras: 0x{1:x2}", iPos, num3);
            for (int i = 0; i < num3; i++)
            {
                ReadCam();
            }
            iPos += 4;
            num = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", iPos, num);
            iPos += 2 * num;
            return iPos;
        }
    }
}