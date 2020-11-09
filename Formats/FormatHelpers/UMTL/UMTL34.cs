using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.UMTL
{
    public class UMTL34 : UMTL00
    {
        public UMTL34(byte[] fileData, int iPos)
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
                iPos += 305;
                var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 20;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += 469;
                var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                var name = readString(int32_4);
                iPos += 124;
                var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += int32_5 * 3;
                var int32_6 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                iPos += int32_6 * 3;
                iPos += 98;
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