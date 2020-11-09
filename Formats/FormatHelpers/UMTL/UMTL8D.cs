using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.UMTL
{
    public class UMTL8D : UMTL00
    {
        public UMTL8D(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index = 0; index < int32_1; ++index)
            {
                iPos += 4;
                iPos += 4;
                iPos += 4;
                iPos += 4;
                iPos += 4;
                iPos += 375;
                var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 20;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 486;
                var int16 = BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var name = readString((int)int16);
                var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += int32_4 * 200;
                var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += int32_5 * 3;
                iPos += 111;
                ColoredConsole.WriteLineInfo("{0:x8}   {4:0000} {1} --> Tex: {2}; Norm: {3}", (object)iPos, (object)name, (object)int32_2, (object)int32_3, (object)index);
                Materials.Add(new Material(name, int32_2, int32_3));
            }
            return iPos;
        }

        protected new string readString(int numberofchars)
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