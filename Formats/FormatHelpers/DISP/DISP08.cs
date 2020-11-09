using System.Collections.Generic;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
    public class DISP08 : DISP0F
    {
        public DISP08(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read()
        {
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}       Name: {1}", (object)iPos, (object)readString(int32_1));
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            var intList = new List<int>();
            for (var index = 0; index < int32_2; ++index)
            {
                BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                intList.Add(int32_3);
            }
            var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            for (var index1 = 0; index1 < int32_4; ++index1)
            {
                var group = new Group();
                BigEndianBitConverter.ToInt16(fileData, iPos);
                iPos += 2;
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                for (var index2 = 0; index2 < int32_3; ++index2)
                {
                    var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    ColoredConsole.WriteWarn("{0}; ", (object)int32_5);
                    iPos += 4;
                    group.Material.Add(int32_5);
                }
                ColoredConsole.WriteLine();
                var int32_6 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                for (var index2 = 0; index2 < int32_6; ++index2)
                {
                    var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    ColoredConsole.WriteDebug("{0} --> {1}; ", (object)int32_5, (object)intList[int32_5]);
                    iPos += 4;
                    group.Parts.Add(intList[int32_5]);
                }
                ColoredConsole.WriteLine();
                Groups.Add(group);
            }
            ColoredConsole.WriteLineError("{0:x8}", (object)iPos);
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