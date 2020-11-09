using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.HGOL
{
    public class HGOL10 : HGOL01
    {
        public HGOL10(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            iPos += 4;
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", (object)iPos, (object)int32_1);
            iPos += 4;
            for (var index = 0; index < int32_1; ++index)
            {
                var int16 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                ColoredConsole.WriteLine("{0:x8}       Name: {1}", (object)iPos, (object)readString((int)int16));
                iPos += 64;
                iPos += 14;
            }
            iPos += 4;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", (object)iPos, (object)int32_2);
            iPos += 4;
            for (var index = 0; index < int32_2; ++index)
                iPos += 64;
            iPos += 4;
            var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", (object)iPos, (object)int32_3);
            iPos += 4;
            for (var index = 0; index < int32_3; ++index)
                iPos += 64;
            return iPos;
        }

        private string readString(int numberofchars)
        {
            var stringBuilder = new StringBuilder();
            for (var index = 0; index < numberofchars; ++index)
            {
                if (fileData[iPos] != (byte)0)
                    stringBuilder.Append((char)fileData[iPos]);
                ++iPos;
            }
            return stringBuilder.ToString();
        }
    }
}