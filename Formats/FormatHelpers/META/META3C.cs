using System.Collections.Generic;
using System.IO;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.META
{
    public class META3C : META0C
    {
        public META3C(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(string directoryname)
        {
            var stringList = new List<string>();
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}  Number of File Names: {1:x8}", (object)iPos, (object)int32_1);
            iPos += 4;
            for (var index = 0; index < int32_1; ++index)
            {
                var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                var str = readString(int32_2);
                stringList.Add(str);
                ColoredConsole.WriteLine("{0:x8}    Name: {1}", (object)iPos, (object)str);
            }
            iPos += 4;
            iPos += 2;
            iPos += 4;
            for (var index = 0; index < stringList.Count; ++index)
            {
                var ddsFileSize = DdsHelper.CalculateDdsFileSize(iPos, fileData);
                ColoredConsole.WriteLine("{0:x8}    Size: {1:x8}", (object)iPos, (object)ddsFileSize);
                var fileStream = File.OpenWrite(directoryname + "\\" + $"{(object)index:0000}_" + Path.GetFileNameWithoutExtension(stringList[index]) + ".dds");
                fileStream.Write(fileData, iPos, ddsFileSize);
                fileStream.Close();
                iPos += ddsFileSize;
            }
            return iPos;
        }
    }
}