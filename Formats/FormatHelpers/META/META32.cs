using System.Collections.Generic;
using System.IO;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.META
{
    public class META32 : META0C
    {
        public META32(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(string directoryname)
        {
            List<string> stringList = new List<string>();
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}  Number of File Names: {1:x8}", (object)this.iPos, (object)int32_1);
            this.iPos += 4;
            for (int index = 0; index < int32_1; ++index)
            {
                int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                string str = this.readString(int32_2);
                stringList.Add(str);
                ColoredConsole.WriteLine("{0:x8}    Name: {1}", (object)this.iPos, (object)str);
            }
            this.iPos += 5;
            for (int index = 0; index < stringList.Count; ++index)
            {
                int ddsFileSize = DdsHelper.CalculateDdsFileSize(this.iPos, this.fileData);
                ColoredConsole.WriteLine("{0:x8}    Size: {1:x8}", (object)this.iPos, (object)ddsFileSize);
                FileStream fileStream = File.OpenWrite(directoryname + "\\" + string.Format("{0:0000}_", (object)index) + Path.GetFileNameWithoutExtension(stringList[index]) + ".dds");
                fileStream.Write(this.fileData, this.iPos, ddsFileSize);
                fileStream.Close();
                this.iPos += ddsFileSize;
            }
            return this.iPos;
        }
    }
}