using System.Collections.Generic;
using System.IO;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.META
{
    public class META0C
    {
        protected byte[] fileData;
        protected int iPos;

        public META0C(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read(string directoryname)
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

        protected string readString(int numberofchars)
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